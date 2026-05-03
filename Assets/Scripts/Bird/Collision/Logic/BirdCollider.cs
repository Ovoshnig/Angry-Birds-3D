public class BirdCollider : ObjectCollider<BirdEntityView>
{
    public BirdCollider(BirdEntityView[] entityViews, CollisionSettings collisionSettings) 
        : base(entityViews, collisionSettings)
    {
    }

    protected override ObjectColliderView GetObjectColliderView(BirdEntityView entityView) =>
        entityView.ColliderView;
}
