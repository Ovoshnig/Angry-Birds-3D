using UnityEngine;

public class PigDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    [field: SerializeField] public DestructionSFXSettings DestructionSFXSettings { get; private set; }

    public HealthModel HealthModel { get; private set; }

    private void Awake() => HealthModel = new HealthModel(_gameSettings.PigSettings.Health);

    public void Damage(float _)
    {
    }

    public void Destroy() => Destroy(gameObject);
}
