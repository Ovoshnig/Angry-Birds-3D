using R3;

public class FeatherParticleViewObjectColliderMediator : Mediator
{
    private readonly FeatherParticleView _featherParticleView;
    private readonly ObjectCollider _objectCollider;

    public FeatherParticleViewObjectColliderMediator(FeatherParticleView featherParticleView,
        ObjectCollider objectCollider)
    {
        _featherParticleView = featherParticleView;
        _objectCollider = objectCollider;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _objectCollider.Collided
            .Where(data => data.EntityView is BirdEntityView)
            .Subscribe(OnCollided)
            .AddTo(disposables);
    }

    private void OnCollided(CollisionData data)
    {
        if (data.EntityView is not BirdEntityView birdEntityView)
            return;

        _featherParticleView.Emit(birdEntityView.transform.position, birdEntityView.FeatherColor, data.Force);
    }
}
