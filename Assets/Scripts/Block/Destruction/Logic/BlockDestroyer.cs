public class BlockDestroyer : ObjectDestroyer<BlockEntityView>
{
    private readonly DestructionPointsSettings _blockPointsSettings;

    public BlockDestroyer(BlockCollider blockCollider, ScoreSettings scoreSettings)
        : base(blockCollider) => _blockPointsSettings = scoreSettings.BlockPointsSettings;

    protected override DestructionPointsSettings DestructionPointsSettings => _blockPointsSettings;

    public override ObjectDestroyerView GetObjectDestroyerView(BlockEntityView entityView) =>
        entityView.DestroyerView;
}
