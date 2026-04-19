using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CollisionView))]
public abstract class BlockDestroyerView : ObjectDestroyerView
{
    private Material _material;

    protected BlockSettings BlockSettings => GameSettings.BlockSettings;

    protected override void Awake()
    {
        base.Awake();

        _material = GetComponent<MeshRenderer>().material;
    }

    public override void Damage(float _)
    {
        float crackAmount = 1f - (HealthModel.Health / MaxHealth);
        _material.SetFloat(BlockDestructionConstants.CrackAmountName, crackAmount);
    }
}
