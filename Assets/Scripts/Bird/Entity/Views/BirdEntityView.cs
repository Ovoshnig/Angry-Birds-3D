using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
[RequireComponent(typeof(BirdDestroyerView))]
[RequireComponent(typeof(BirdPowerView))]
public class BirdEntityView : CollidableEntityView
{
    [field: SerializeField] public BirdSFXSettings SFXSettings { get; private set; }
    [field: SerializeField] public PointsSettings PointsSettings { get; private set; }

    public BirdFlyerView FlyerView { get; private set; }
    public BirdDestroyerView DestroyerView { get; private set; }
    public BirdPowerView PowerView { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        FlyerView = GetComponent<BirdFlyerView>();
        DestroyerView = GetComponent<BirdDestroyerView>();
        PowerView = GetComponent<BirdPowerView>();
    }
}
