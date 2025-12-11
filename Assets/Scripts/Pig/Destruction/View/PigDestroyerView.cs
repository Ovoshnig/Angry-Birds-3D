using UnityEngine;

public class PigDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    [field: SerializeField] public DestructionSFXSettings DestructionSFXSettings { get; private set; }

    public PigHealthModel HealthModel { get; private set; }

    private void Awake() => HealthModel = new PigHealthModel(_gameSettings.PigSettings.Health);

    public void Damage(float _)
    {
    }

    public void Destroy() => Destroy(gameObject);
}
