using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
[RequireComponent(typeof(BirdDestroyerView))]
public class BirdEntityView : CollidableEntityView
{
    [field: SerializeField] public BirdSFXSettings SFXSettings { get; private set; }

    public BirdFlyerView FlyerView { get; private set; }
    public BirdDestroyerView DestroyerView { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        FlyerView = GetComponent<BirdFlyerView>();
        DestroyerView = GetComponent<BirdDestroyerView>();
    }
}
