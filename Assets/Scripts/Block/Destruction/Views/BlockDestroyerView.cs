using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BlockDestroyerView : ObjectDestroyerView
{
    private Material _material;

    private void Awake() => _material = GetComponent<MeshRenderer>().material;

    public override void VisualizeDamage(float health, float maxHealth)
    {
        float crackAmount = 1f - (health / maxHealth);
        _material.SetFloat(BlockDestructionConstants.CrackAmountName, crackAmount);
    }
}
