using R3;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CollisionView))]
public abstract class BlockDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private readonly Subject<int> _destroyed = new();

    private Material _material;
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

    public Observable<int> Destroyed => _destroyed;

    protected BlockSettings BlockSettings => _gameSettings.BlockSettings;
    protected abstract float DamageMultiplier { get; }

    private void Awake() => _material = GetComponent<MeshRenderer>().material;

    public void Damage(float rawDamage)
    {
        float _crackAmount = _material.GetFloat(BlockDestructionConstants.CrackAmountName);
        _crackAmount += DamageMultiplier * rawDamage;

        if (_crackAmount >= 1f)
        {
            _destroyed.OnNext(_gameSettings.ScoreSettings.BlockPoints);
            Destroy(gameObject);
        }
        else
        {
            _material.SetFloat(BlockDestructionConstants.CrackAmountName, _crackAmount);
        }
    }
}
