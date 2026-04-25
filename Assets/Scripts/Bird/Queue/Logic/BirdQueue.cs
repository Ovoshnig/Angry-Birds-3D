using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdQueue : IPostInitializable, IDisposable
{
    private readonly Queue<BirdEntityView> _queue;
    private readonly BirdFlyer _birdFlyer;
    private readonly Subject<BirdEntityView> _birdDequeued = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BirdQueue(BirdEntityView[] entityViews, BirdFlyer birdFlyer)
    {
        _queue = new Queue<BirdEntityView>(entityViews);
        _birdFlyer = birdFlyer;
    }

    public Observable<BirdEntityView> BirdDequeued => _birdDequeued;

    public void PostInitialize()
    {
        TryDequeueBird();

        _birdFlyer.BirdCollided
            .Subscribe(_ => TryDequeueBird())
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

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
