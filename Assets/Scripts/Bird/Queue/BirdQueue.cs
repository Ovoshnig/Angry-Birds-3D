using R3;
using System.Collections.Generic;

public class BirdQueue
{
    private readonly Queue<BirdFlyerView> _birdFlyerQueue;
    private readonly Subject<BirdFlyerView> _birdDequeued = new();

    public BirdQueue(BirdFlyerView[] birdFlyerViews) =>
        _birdFlyerQueue = new Queue<BirdFlyerView>(birdFlyerViews);

    public Observable<BirdFlyerView> BirdDequeued => _birdDequeued;

    public void DequeueBird()
    {
        if (_birdFlyerQueue.TryDequeue(out BirdFlyerView birdFlyerView))
        {
            birdFlyerView.transform.SetParent(null);
            _birdDequeued.OnNext(birdFlyerView);
        }
    }
}
