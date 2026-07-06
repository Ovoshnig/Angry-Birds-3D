using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdTracker : IStartable, IDisposable
{
    private readonly BirdFlyer _birdFlyer;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly ReactiveProperty<int> _birdCount = new();
    private readonly ReactiveProperty<int> _unlaunchedBirdCount = new();
    private readonly CompositeDisposable _disposables = new();

    public BirdTracker(BirdFlyer birdFlyer,
        BirdDestroyer birdDestroyer,
        IReadOnlyList<BirdEntityView> birdEntityViews)
    {
        _birdFlyer = birdFlyer;
        _birdDestroyer = birdDestroyer;
        _birdCount.Value = birdEntityViews.Count;
        _unlaunchedBirdCount.Value = birdEntityViews.Count;

        BirdsLeft = _birdCount
            .Where(count => count == 0)
            .Take(1)
            .AsUnitObservable()
            .Share();
    }

    public ReadOnlyReactiveProperty<int> BirdCount => _birdCount;
    public ReadOnlyReactiveProperty<int> UnlaunchedBirdCount => _unlaunchedBirdCount;
    public Observable<Unit> BirdsLeft { get; }
    public bool AnyBirds => _birdCount.Value > 0;
    public bool AnyUnlaunchedBirds => _unlaunchedBirdCount.Value > 0;

    public void Start()
    {
        _birdFlyer.FlightStarted
            .Subscribe(_ => _unlaunchedBirdCount.Value--)
            .AddTo(_disposables);

        _birdDestroyer.Destroyed
            .Subscribe(_ => _birdCount.Value--)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _birdCount.Dispose();
        _unlaunchedBirdCount.Dispose();
    }
}
