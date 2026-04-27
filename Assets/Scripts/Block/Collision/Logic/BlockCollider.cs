public class BlockCollider : ObjectCollider<BlockEntityView>
{
    public BlockCollider(BlockEntityView[] entityViews, CollisionSettings collisionSettings)
        : base(entityViews, collisionSettings)
    {
    }

    protected override ObjectColliderView GetObjectColliderView(BlockEntityView entityView) =>
        entityView.ColliderView;
}
