public class BlockDestroyer : ObjectDestroyer<BlockEntityView>
{
    public BlockDestroyer(BlockCollider blockCollider) : base(blockCollider)
    {
    }

    protected override ObjectDestroyerView GetObjectDestroyerView(BlockEntityView entityView) =>
        entityView.DestroyerView;
}
