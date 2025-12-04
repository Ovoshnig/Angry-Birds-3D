public class BlockCollisionMediator : CollisionMediator<BlockDestructionView>
{
    private readonly BlockDestructionView[] _blockDestructionViews;

    public BlockCollisionMediator(BlockCollisionReporter blockCollisionReporter,
        BlockDestructionView[] blockDestructionViews)
        : base(blockCollisionReporter) => _blockDestructionViews = blockDestructionViews;

    public override void Initialize()
    {
        foreach (var blockDestructionView in _blockDestructionViews)
        {
            CollisionView blockCollisionView = blockDestructionView.CollisionView;
            Subscribe(blockDestructionView, blockCollisionView);
        }
    }
}
