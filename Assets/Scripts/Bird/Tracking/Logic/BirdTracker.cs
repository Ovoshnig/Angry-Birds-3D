using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdTracker : IStartable, IDisposable
{
    private readonly BirdFlyer _birdFlyer;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly ReactiveProperty<int> _birdCount = new();
    private readonly ReactiveProperty<bool> _isBirdLaunched = new(false);
    private readonly CompositeDisposable _disposables = new();

    public BirdTracker(BirdFlyer birdFlyer,
        BirdDestroyer birdDestroyer,
        IReadOnlyList<BirdEntityView> birdEntityViews)
    {
        _birdFlyer = birdFlyer;
        _birdDestroyer = birdDestroyer;
        _birdCount.Value = birdEntityViews.Count;

        BirdsLeft = _birdCount
            .Where(count => count == 0)
            .AsUnitObservable()
            .Share();
    }

    public ReadOnlyReactiveProperty<int> BirdCount => _birdCount;
    public ReadOnlyReactiveProperty<bool> IsBirdLaunched => _isBirdLaunched;
    public Observable<Unit> BirdsLeft { get; }
    public bool AnyBirds => _birdCount.Value > 0;

    public void Start()
    {
        _birdDestroyer.Destroyed
            .Subscribe(_ =>
            {
                _birdCount.Value--;
                _isBirdLaunched.Value = false;
            })
            .AddTo(_disposables);

        _birdFlyer.FlightStarted
            .Subscribe(_ => _isBirdLaunched.Value = true)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _birdCount.Dispose();
        _isBirdLaunched.Dispose();
    }
}
