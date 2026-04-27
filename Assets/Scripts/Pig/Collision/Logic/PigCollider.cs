public class PigCollider : ObjectCollider<PigEntityView>
{
    public PigCollider(PigEntityView[] entityViews, CollisionSettings collisionSettings)
        : base(entityViews, collisionSettings)
    {
    }

    protected override ObjectColliderView GetObjectColliderView(PigEntityView entityView) =>
        entityView.ColliderView;
}
