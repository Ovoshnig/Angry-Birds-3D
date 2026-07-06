using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class ActivityTracker : IStartable, IDisposable
{
    private readonly BirdFlyer _birdFlyer;
    private readonly ObjectCollider _objectCollider;
    private readonly LevelStateTrackingSettings _stateTrackingSettings;
    private readonly ReactiveProperty<bool> _isActive = new(false);
    private readonly CompositeDisposable _disposables = new();

    public ActivityTracker(BirdFlyer birdFlyer, ObjectCollider objectCollider,
        LevelStateTrackingSettings stateTrackingSettings)
    {
        _birdFlyer = birdFlyer;
        _objectCollider = objectCollider;
        _stateTrackingSettings = stateTrackingSettings;

        CalmedDown = _isActive
            .Pairwise()
            .Where(isActive => isActive.Previous && !isActive.Current)
            .AsUnitObservable()
            .Share();
    }

    public ReadOnlyReactiveProperty<bool> IsActive => _isActive;
    public Observable<Unit> CalmedDown { get; }

    public void Start()
    {
        _birdFlyer.FlightInterrupted
            .Subscribe(_ => OnFlightInterrupted())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _isActive.Dispose();
    }

    private void OnFlightInterrupted()
    {
        _isActive.Value = true;

        _objectCollider.Collided
            .Select(data => Unit.Default)
            .Prepend(Unit.Default)
            .Timeout(TimeSpan.FromSeconds(_stateTrackingSettings.ActivityTimeout))
            .Subscribe(onNext: delegate { },
                onCompleted: result =>
                {
                    if (result.IsFailure)
                        _isActive.Value = false;
                })
            .AddTo(_disposables);
    }
}
