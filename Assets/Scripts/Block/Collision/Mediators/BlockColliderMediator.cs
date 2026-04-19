using VContainer.Unity;

public class BlockColliderMediator : ObjectColliderMediator<BlockEntityView>, IStartable
{
    private readonly BlockEntityView[] _blockEntityViews;

    public BlockColliderMediator(BlockCollider blockCollisionReporter,
        BlockEntityView[] blockEntityViews)
        : base(blockCollisionReporter) => _blockEntityViews = blockEntityViews;

    public override void Initialize()
    {
    }

    public void Start()
    {
        foreach (var blockEntityView in _blockEntityViews)
        {
            ObjectColliderView colliderView = blockEntityView.ColliderView;
            Subscribe(blockEntityView, colliderView);
        }
    }
}
