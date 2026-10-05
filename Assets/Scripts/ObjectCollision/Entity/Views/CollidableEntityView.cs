using Ovoshnig.ObjectCollision.Collision;
using UnityEngine;

namespace Ovoshnig.ObjectCollision.Entity
{
    [RequireComponent(typeof(ObjectColliderView))]
    public abstract class CollidableEntityView : MonoBehaviour
    {
        public ObjectColliderView ColliderView { get; private set; }

        protected virtual void Awake() => ColliderView = GetComponent<ObjectColliderView>();
    }
}
