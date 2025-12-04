using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BlockDestroyerView : MonoBehaviour
{
    [field: SerializeField] public CollisionView CollisionView { get; private set; }

    private Material _material;

    private void Awake() => _material = GetComponent<MeshRenderer>().material;

    public void Damage(float damage)
    {
        float _crackAmount = _material.GetFloat(BlockDestructionConstants.CrackAmountName);
        _crackAmount += damage;

        if (_crackAmount >= 1)
            Destroy(gameObject);
        else
            _material.SetFloat(BlockDestructionConstants.CrackAmountName, _crackAmount);
    }
}
