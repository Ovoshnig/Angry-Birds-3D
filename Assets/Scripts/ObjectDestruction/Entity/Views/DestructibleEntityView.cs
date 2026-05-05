using UnityEngine;

[RequireComponent(typeof(ObjectColliderView))]
public abstract class DestructibleEntityView : MonoBehaviour
{
    public ObjectColliderView ColliderView { get; private set; }
    public ObjectDestroyerView DestroyerView { get; protected set; }

    protected virtual void Awake() => ColliderView = GetComponent<ObjectColliderView>();
}
