using VContainer.Unity;

public class BlockCollisionMediator : CollisionMediator<BlockEntityView>, IStartable
{
    private readonly BlockEntityView[] _blockEntityViews;

    public BlockCollisionMediator(BlockCollisionReporter blockCollisionReporter,
        BlockEntityView[] blockEntityViews)
        : base(blockCollisionReporter) => _blockEntityViews = blockEntityViews;

    public override void Initialize()
    {
    }

    public void Start()
    {
        foreach (var blockEntityView in _blockEntityViews)
        {
            CollisionView collisionView = blockEntityView.CollisionView;
            Subscribe(blockEntityView, collisionView);
        }
    }
}
