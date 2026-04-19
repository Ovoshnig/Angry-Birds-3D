using UnityEngine;

public abstract class ObjectDestroyerView : MonoBehaviour
{
    [field: SerializeField] public DestructionSFXSettings DestructionSFXSettings { get; private set; }

    public HealthModel HealthModel { get; private set; }

    [field: SerializeField] protected GameSettings GameSettings { get; private set; }

    protected abstract float MaxHealth { get; }

    protected virtual void Awake() => HealthModel = new HealthModel(MaxHealth);

    public abstract void Damage(float value);

    public virtual void Destroy() => Destroy(gameObject);
}
