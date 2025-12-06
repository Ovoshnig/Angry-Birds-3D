using UnityEngine;

[RequireComponent(typeof(CollisionView))]
public class PigDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private CollisionView _collisionView = null;

    public CollisionView CollisionView
    {
        get
        {
            if (_collisionView == null)
                _collisionView = GetComponent<CollisionView>();

            return _collisionView;
        }
    }

    public PigHealthModel PigHealthModel { get; private set; }

    private void Awake() => PigHealthModel = new PigHealthModel(_gameSettings.PigSettings.Health);

    public void Damage(float _)
    {
    }

    public void Destroy() => Destroy(gameObject);
}
