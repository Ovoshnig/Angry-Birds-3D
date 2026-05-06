using UnityEngine;

[RequireComponent(typeof(ObjectColliderView))]
public abstract class CollidableEntityView : MonoBehaviour
{
    public ObjectColliderView ColliderView { get; private set; }

    protected virtual void Awake() => ColliderView = GetComponent<ObjectColliderView>();
}
