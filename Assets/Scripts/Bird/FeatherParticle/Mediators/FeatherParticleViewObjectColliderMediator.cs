using AngryBirds3D.Bird.Entity;
using Ovoshnig.Mediation;
using Ovoshnig.ObjectCollision.Collision;
using R3;

namespace AngryBirds3D.Bird.FeatherParticle;

public class FeatherParticleViewObjectColliderMediator(FeatherParticleView featherParticleView,
    ObjectCollider objectCollider) : Mediator
{
    private readonly FeatherParticleView _featherParticleView = featherParticleView;
    private readonly ObjectCollider _objectCollider = objectCollider;

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
