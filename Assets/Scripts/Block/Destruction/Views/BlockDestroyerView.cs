using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ObjectColliderView))]
public class BlockDestroyerView : ObjectDestroyerView
{
    private Material _material;

    protected override void Awake()
    {
        base.Awake();

        _material = GetComponent<MeshRenderer>().material;
    }

    public override void Damage(float _)
    {
        float crackAmount = 1f - (HealthModel.Health / Settings.MaxHealth);
        _material.SetFloat(BlockDestructionConstants.CrackAmountName, crackAmount);
    }
}
