using UnityEngine;

[RequireComponent(typeof(CollisionView))]
[RequireComponent(typeof(PigSFXPlayerView))]
public class PigDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private CollisionView _collisionView = null;
    private PigSFXPlayerView _sfxPlayerView = null;

    public CollisionView CollisionView
    {
        get
        {
            if (_collisionView == null)
                _collisionView = GetComponent<CollisionView>();

            return _collisionView;
        }
    }

    public PigSFXPlayerView SFXPlayerView
    {
        get
        {
            if (_sfxPlayerView == null)
                _sfxPlayerView = GetComponent<PigSFXPlayerView>();

            return _sfxPlayerView;
        }
    }

    public PigHealthModel HealthModel { get; private set; }

    private void Awake() => HealthModel = new PigHealthModel(_gameSettings.PigSettings.Health);

    public void Damage(float _)
    {
    }

    public void Destroy() => Destroy(gameObject);
}
