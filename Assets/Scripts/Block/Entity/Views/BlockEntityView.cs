using UnityEngine;

[RequireComponent(typeof(CollisionView))]
[RequireComponent(typeof(BlockSFXPlayerView))]
public class BlockEntityView : MonoBehaviour
{
    public CollisionView CollisionView { get; private set; }
    public BlockDestroyerView DestroyerView { get; private set; }
    public BlockSFXPlayerView SFXPlayerView { get; private set; }

    private void Awake()
    {
        CollisionView = GetComponent<CollisionView>();
        DestroyerView = GetComponent<BlockDestroyerView>();
        SFXPlayerView = GetComponent<BlockSFXPlayerView>();
    }
}
