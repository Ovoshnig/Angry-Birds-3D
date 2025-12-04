using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class BlockDestroyerView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private Material _material;

    [field: SerializeField] public CollisionView CollisionView { get; private set; }

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
