using VContainer.Unity;

public class PigColliderMediator : ObjectColliderMediator<PigEntityView>, IStartable
{
    private readonly PigEntityView[] _pigEntityViews;

    public PigColliderMediator(PigCollider pigCollisionReporter,
        PigEntityView[] pigEntityViews)
        : base(pigCollisionReporter) => _pigEntityViews = pigEntityViews;

    public override void Initialize()
    {
    }

    public void Start()
    {
        foreach (var pigEntityView in _pigEntityViews)
        {
            ObjectColliderView colliderView = pigEntityView.ColliderView;
            Subscribe(pigEntityView, colliderView);
        }
    }
}
