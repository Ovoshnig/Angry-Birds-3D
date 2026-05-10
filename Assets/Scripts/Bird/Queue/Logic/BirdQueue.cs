using R3;
using System;
using System.Collections.Generic;
using VContainer.Unity;

public class BirdQueue : IStartable, IDisposable
{
    private readonly Queue<BirdEntityView> _queue;
    private readonly Subject<BirdEntityView> _birdDequeued = new();

    public BirdQueue(IReadOnlyList<BirdEntityView> entityViews) =>
        _queue = new Queue<BirdEntityView>(entityViews);

    public Observable<BirdEntityView> BirdDequeued => _birdDequeued;

    public void Start()
    {
        foreach (var entityView in _queue)
            entityView.FlyerView.Rigidbody.detectCollisions = false;
    }

    public void Dispose() => _birdDequeued.Dispose();

    public bool TryDequeueBird()
    {
        if (!_queue.TryDequeue(out BirdEntityView entityView))
            return false;

        entityView.transform.SetParent(null);
        _birdDequeued.OnNext(entityView);
        return true;
    }
}
