using UnityEngine;

[RequireComponent(typeof(ObjectColliderView))]
[RequireComponent(typeof(PigDestroyerView))]
public class PigEntityView : MonoBehaviour
{
    public ObjectColliderView ColliderView { get; private set; }
    public PigDestroyerView DestroyerView { get; private set; }

    private void Awake()
    {
        ColliderView = GetComponent<ObjectColliderView>();
        DestroyerView = GetComponent<PigDestroyerView>();
    }
}
