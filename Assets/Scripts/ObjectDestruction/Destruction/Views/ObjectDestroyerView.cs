using UnityEngine;

public abstract class ObjectDestroyerView : MonoBehaviour
{
    [field: SerializeField] public DestructionSettings Settings { get; private set; }

    public HealthModel HealthModel { get; private set; }

    protected virtual void Awake() => HealthModel = new HealthModel(Settings.MaxHealth);

    public abstract void Damage(float value);

    public virtual void Destroy() => Destroy(gameObject);
}
