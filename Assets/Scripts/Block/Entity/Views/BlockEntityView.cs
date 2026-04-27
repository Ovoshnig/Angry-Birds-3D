using UnityEngine;

[RequireComponent(typeof(ObjectColliderView))]
[RequireComponent(typeof(BlockDestroyerView))]
public class BlockEntityView : MonoBehaviour
{
    public ObjectColliderView ColliderView { get; private set; }
    public BlockDestroyerView DestroyerView { get; private set; }

    private void Awake()
    {
        ColliderView = GetComponent<ObjectColliderView>();
        DestroyerView = GetComponent<BlockDestroyerView>();
    }
}
