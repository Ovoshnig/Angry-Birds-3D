public class BlockDestroyer : ObjectDestroyer<BlockEntityView>
{
    private readonly BlockSettings _blockSettings;
    private readonly DestructionPointsSettings _blockPointsSettings;

    public BlockDestroyer(BlockCollider collisionReporter,
        BlockSettings blockSettings, ScoreSettings scoreSettings) : base(collisionReporter)
    {
        _blockSettings = blockSettings;
        _blockPointsSettings = scoreSettings.BlockPointsSettings;
    }

    protected override DestructionPointsSettings DestructionPointsSettings => _blockPointsSettings;
    protected override float DamageThreshold => _blockSettings.DamageThreshold;

    public override ObjectDestroyerView GetObjectDestroyerView(BlockEntityView entityView) =>
        entityView.DestroyerView;
}
