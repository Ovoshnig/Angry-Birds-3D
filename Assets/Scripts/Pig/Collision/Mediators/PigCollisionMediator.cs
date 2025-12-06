public class PigCollisionMediator : CollisionMediator<PigDestroyerView>
{
    private readonly PigDestroyerView[] _pigDestroyerViews;

    public PigCollisionMediator(PigCollisionReporter pigCollisionReporter,
        PigDestroyerView[] pigDestroyerViews)
        : base(pigCollisionReporter) => _pigDestroyerViews = pigDestroyerViews;

    public override void Initialize()
    {
        foreach (var pigDestroyerView in _pigDestroyerViews)
        {
            CollisionView pigCollisionView = pigDestroyerView.CollisionView;
            Subscribe(pigDestroyerView, pigCollisionView);
        }
    }
}
