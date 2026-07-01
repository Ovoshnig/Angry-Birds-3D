using UnityEngine;

[RequireComponent(typeof(BlockDestroyerView))]
public class BlockEntityView : DestructibleEntityView
{
    [field: SerializeField] public BlockParticleProfile ParticleProfile { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DestroyerView = GetComponent<BlockDestroyerView>();
    }
}
