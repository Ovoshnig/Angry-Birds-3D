using UnityEngine;

public class PigDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    public PigHealthModel HealthModel { get; private set; }

    private void Awake() => HealthModel = new PigHealthModel(_gameSettings.PigSettings.Health);

    public void Damage(float _)
    {
    }

    public void Destroy() => Destroy(gameObject);
}
