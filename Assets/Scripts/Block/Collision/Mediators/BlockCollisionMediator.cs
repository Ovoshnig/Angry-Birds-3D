public class BlockCollisionMediator : CollisionMediator<BlockDestroyerView>
{
    private readonly BlockDestroyerView[] _blockDestroyerViews;

    public BlockCollisionMediator(BlockCollisionReporter blockCollisionReporter,
        BlockDestroyerView[] blockDestroyerViews)
        : base(blockCollisionReporter) => _blockDestroyerViews = blockDestroyerViews;

    public override void Initialize()
    {
        foreach (var blockDestroyerView in _blockDestroyerViews)
        {
            CollisionView blockCollisionView = blockDestroyerView.CollisionView;
            Subscribe(blockDestroyerView, blockCollisionView);
        }
    }
}
