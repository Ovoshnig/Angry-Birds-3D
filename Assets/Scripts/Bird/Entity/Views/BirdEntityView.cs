using UnityEngine;

[RequireComponent(typeof(BirdFlyerView))]
[RequireComponent(typeof(BirdDestroyerView))]
[RequireComponent(typeof(BirdPowerView))]
[RequireComponent(typeof(BirdAnimatorView))]
public class BirdEntityView : CollidableEntityView
{
    [field: SerializeField] public BirdSfxProfile SfxProfile { get; private set; }
    [field: SerializeField] public PointsSettings PointsSettings { get; private set; }
    [field: SerializeField] public Color FeatherColor { get; private set; }

    public BirdFlyerView FlyerView { get; private set; }
    public BirdDestroyerView DestroyerView { get; private set; }
    public BirdPowerView PowerView { get; private set; }
    public BirdAnimatorView AnimatorView { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        FlyerView = GetComponent<BirdFlyerView>();
        DestroyerView = GetComponent<BirdDestroyerView>();
        PowerView = GetComponent<BirdPowerView>();
        AnimatorView = GetComponent<BirdAnimatorView>();
    }
}
