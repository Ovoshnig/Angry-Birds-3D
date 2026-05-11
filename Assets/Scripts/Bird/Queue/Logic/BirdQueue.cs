using System.Collections.Generic;
using VContainer.Unity;

public class BirdQueue : IStartable
{
    private readonly Queue<BirdEntityView> _queue;

    public BirdQueue(IReadOnlyList<BirdEntityView> entityViews) =>
        _queue = new Queue<BirdEntityView>(entityViews);

    public void Start()
    {
        foreach (var entityView in _queue)
            entityView.FlyerView.Rigidbody.detectCollisions = false;
    }

    public bool TryDequeueBird(out BirdEntityView birdEntityView)
    {
        if (_queue.TryDequeue(out BirdEntityView entityView))
        {
            entityView.transform.SetParent(null);
            birdEntityView = entityView;
            return true;
        }

        birdEntityView = null;
        return false;
    }
}
