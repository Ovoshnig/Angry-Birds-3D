using UnityEngine;

namespace Ovoshnig.ObjectDestruction.Destruction
{
    public abstract class ObjectDestroyerView : MonoBehaviour
    {
        public abstract void VisualizeDamage(Vector3 worldPoint, float health, float maxHealth);

        public virtual void Destroy() => Destroy(gameObject);
    }
}
