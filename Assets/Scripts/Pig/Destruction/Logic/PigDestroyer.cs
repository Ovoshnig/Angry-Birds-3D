public class PigDestroyer : ObjectDestroyer<PigEntityView>
{
    private readonly PigSettings _pigSettings;
    private readonly DestructionPointsSettings _pigPointsSettings;

    public PigDestroyer(PigCollider collisionReporter,
        ScoreSettings scoreSettings, PigSettings pigSettings) : base(collisionReporter)
    {
        _pigSettings = pigSettings;
        _pigPointsSettings = scoreSettings.PigPointsSettings;
    }

    protected override DestructionPointsSettings DestructionPointsSettings => _pigPointsSettings;
    protected override float DamageThreshold => _pigSettings.DamageThreshold;

    public override ObjectDestroyerView GetObjectDestroyerView(PigEntityView entityView) =>
        entityView.DestroyerView;
}
