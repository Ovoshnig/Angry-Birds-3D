using AngryBirds3D.Bird.Entity;
using System.Collections.Generic;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Queue;

public class BirdQueue(IReadOnlyList<BirdEntityView> entityViews) : IStartable
{
    private readonly Queue<BirdEntityView> _queue = new(entityViews);

    public bool Any => _queue.Count != 0;

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
