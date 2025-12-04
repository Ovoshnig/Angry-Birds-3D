using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CollisionView))]
public abstract class BlockDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

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

    protected BlockSettings BlockSettings => _gameSettings.BlockSettings;
    protected abstract float DamageMultiplier { get; }

    private void Awake() => _material = GetComponent<MeshRenderer>().material;

    public void Damage(float rawDamage)
    {
        float _crackAmount = _material.GetFloat(BlockDestructionConstants.CrackAmountName);
        _crackAmount += DamageMultiplier * rawDamage;

        if (_crackAmount >= 1f)
            Destroy(gameObject);
        else
            _material.SetFloat(BlockDestructionConstants.CrackAmountName, _crackAmount);
    }
}
