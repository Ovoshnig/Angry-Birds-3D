public class BirdColliderMediator : ObjectColliderMediator<BirdEntityView>
{
    private readonly BirdEntityView[] _entityViews;

    public BirdColliderMediator(BirdCollider birdCollider, BirdEntityView[] entityViews)
        : base(birdCollider) => _entityViews = entityViews;

    public override void Initialize()
    {
        foreach (var entityView in _entityViews)
            Subscribe(entityView, entityView.ColliderView);
    }
}
