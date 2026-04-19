public class PigDestroyer : ObjectDestroyer<PigEntityView>
{
    private readonly DestructionPointsSettings _pigPointsSettings;

    public PigDestroyer(PigCollider pigCollider, ScoreSettings scoreSettings)
        : base(pigCollider) => _pigPointsSettings = scoreSettings.PigPointsSettings;

    protected override DestructionPointsSettings DestructionPointsSettings => _pigPointsSettings;

    protected override ObjectDestroyerView GetObjectDestroyerView(PigEntityView entityView) =>
        entityView.DestroyerView;
}
