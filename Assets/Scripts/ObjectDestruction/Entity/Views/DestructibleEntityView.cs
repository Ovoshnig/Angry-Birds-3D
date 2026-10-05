using Ovoshnig.ObjectCollision.Entity;
using Ovoshnig.ObjectDestruction.Destruction;
using Ovoshnig.ObjectHealth.Health;
using UnityEngine;

namespace Ovoshnig.ObjectDestruction.Entity
{
    public abstract class DestructibleEntityView : CollidableEntityView
    {
        [field: SerializeField] public DestructionProfile DestructionProfile { get; private set; }

        public ObjectDestroyerView DestroyerView { get; protected set; }
        public HealthModel HealthModel { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            HealthModel = new HealthModel(DestructionProfile.MaxHealth);
        }
    }
}
