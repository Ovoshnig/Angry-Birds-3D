using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdQueue : IPostInitializable, IDisposable
{
    private readonly Queue<BirdFlyerView> _birdFlyerQueue;
    private readonly BirdFlyer _birdFlyer;
    private readonly Subject<BirdFlyerView> _birdDequeued = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BirdQueue(BirdFlyerView[] birdFlyerViews, BirdFlyer birdFlyer)
    {
        _birdFlyerQueue = new Queue<BirdFlyerView>(birdFlyerViews);
        _birdFlyer = birdFlyer;
    }

    public Observable<BirdFlyerView> BirdDequeued => _birdDequeued;

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
        if (_birdFlyerQueue.TryDequeue(out BirdFlyerView birdFlyerView))
        {
            birdFlyerView.transform.SetParent(null);
            _birdDequeued.OnNext(birdFlyerView);
            return true;
        }

        return false;
    }
}
