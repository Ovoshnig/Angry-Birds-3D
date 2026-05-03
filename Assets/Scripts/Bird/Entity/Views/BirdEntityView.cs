using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
[RequireComponent(typeof(ObjectColliderView))]
[RequireComponent(typeof(BirdDestroyerView))]
public class BirdEntityView : MonoBehaviour
{
    [field: SerializeField] public BirdSFXSettings SFXSettings { get; private set; }

    public BirdFlyerView FlyerView { get; private set; }
    public ObjectColliderView ColliderView { get; private set; }
    public BirdDestroyerView DestroyerView { get; private set; }

    private void Awake()
    {
        FlyerView = GetComponent<BirdFlyerView>();
        ColliderView = GetComponent<ObjectColliderView>();
        DestroyerView = GetComponent<BirdDestroyerView>();
    }
}
