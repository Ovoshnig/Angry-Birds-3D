using UnityEngine;

[RequireComponent(typeof(CollisionView))]
public class BlockEntityView : MonoBehaviour
{
    public CollisionView CollisionView { get; private set; }
    public BlockDestroyerView DestroyerView { get; private set; }

    private void Awake()
    {
        CollisionView = GetComponent<CollisionView>();
        DestroyerView = GetComponent<BlockDestroyerView>();
    }
}
