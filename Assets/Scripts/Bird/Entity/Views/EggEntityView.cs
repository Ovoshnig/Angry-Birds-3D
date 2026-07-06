using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ObjectColliderView))]
public class EggEntityView : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public ObjectColliderView ColliderView { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        ColliderView = GetComponent<ObjectColliderView>();
    }
}
