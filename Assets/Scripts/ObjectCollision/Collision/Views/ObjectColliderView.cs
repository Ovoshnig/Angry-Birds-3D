using R3;
using R3.Triggers;
using UnityEngine;

namespace Ovoshnig.ObjectCollision.Collision
{
    [RequireComponent(typeof(Collider))]
    public class ObjectColliderView : MonoBehaviour
    {
        public Observable<UnityEngine.Collision> Collided { get; private set; }

        private void Awake() => Collided = gameObject.OnCollisionEnterAsObservable();
    }
}
