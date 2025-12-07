using VContainer.Unity;

public class PigCollisionMediator : CollisionMediator<PigEntityView>, IStartable
{
    private readonly PigEntityView[] _pigEntityViews;

    public PigCollisionMediator(PigCollisionReporter pigCollisionReporter,
        PigEntityView[] pigEntityViews)
        : base(pigCollisionReporter) => _pigEntityViews = pigEntityViews;

    public override void Initialize()
    {
    }

    public void Start()
    {
        foreach (var pigEntityView in _pigEntityViews)
        {
            CollisionView collisionView = pigEntityView.CollisionView;
            Subscribe(pigEntityView, collisionView);
        }
    }
}
