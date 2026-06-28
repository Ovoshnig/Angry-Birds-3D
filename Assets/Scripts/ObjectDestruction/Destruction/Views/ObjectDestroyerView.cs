using UnityEngine;

public abstract class ObjectDestroyerView : MonoBehaviour
{
    public abstract void VisualizeDamage(float health, float maxHealth);

    public virtual void Destroy() => Destroy(gameObject);
}
