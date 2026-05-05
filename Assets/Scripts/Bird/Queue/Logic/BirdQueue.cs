using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdQueue : IStartable, IDisposable
{
    private readonly Queue<BirdEntityView> _queue;
    private readonly BirdFlyer _birdFlyer;
    private readonly Subject<BirdEntityView> _birdDequeued = new();
    private readonly CompositeDisposable _disposables = new();

    public BirdQueue(IReadOnlyList<BirdEntityView> entityViews, BirdFlyer birdFlyer)
    {
        _queue = new Queue<BirdEntityView>(entityViews);
        _birdFlyer = birdFlyer;
    }

    public Observable<BirdEntityView> BirdDequeued => _birdDequeued;

    public void Start()
    {
        foreach (var entityView in _queue)
            entityView.FlyerView.Rigidbody.detectCollisions = false;

        _birdFlyer.BirdCollided
            .Subscribe(_ => TryDequeueBird())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _birdDequeued.Dispose();
    }

    public bool TryDequeueBird()
    {
        if (_queue.TryDequeue(out BirdEntityView entityView))
        {
            entityView.transform.SetParent(null);
            _birdDequeued.OnNext(entityView);
            return true;
        }

        return false;
    }
}
