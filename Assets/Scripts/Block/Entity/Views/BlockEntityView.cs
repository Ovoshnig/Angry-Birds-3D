using UnityEngine;

[RequireComponent(typeof(BlockDestroyerView))]
public class BlockEntityView : DestructibleEntityView
{
    protected override void Awake()
    {
        base.Awake();
        DestroyerView = GetComponent<BlockDestroyerView>();
    }
}
