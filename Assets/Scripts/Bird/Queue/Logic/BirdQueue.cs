using AngryBirds3D.Bird.Entity;
using System.Collections.Generic;
using System.Linq;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Queue
{
    public class BirdQueue : IStartable
    {
        private readonly Queue<BirdEntityView> _queue;

        public BirdQueue(IReadOnlyList<BirdEntityView> entityViews) =>
            _queue = new Queue<BirdEntityView>(entityViews);

        public bool Any => _queue.Any();

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
}
