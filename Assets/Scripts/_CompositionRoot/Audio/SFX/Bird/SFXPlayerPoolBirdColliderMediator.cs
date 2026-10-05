using AngryBirds3D.Bird.Entity;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using Ovoshnig.ObjectCollision.Collision;
using R3;

namespace AngryBirds3D.Composition
{
    public class SFXPlayerPoolBirdColliderMediator : Mediator
    {
        private readonly SFXPlayerObjectPool _playerObjectPool;
        private readonly ObjectCollider _objectCollider;
    
        public SFXPlayerPoolBirdColliderMediator(SFXPlayerObjectPool playerObjectPool,
            ObjectCollider objectCollider)
        {
            _playerObjectPool = playerObjectPool;
            _objectCollider = objectCollider;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _objectCollider.Collided
                .Subscribe(data =>
                {
                    if (data.EntityView is BirdEntityView entityView && data.Type == CollisionType.Damage)
                        _playerObjectPool.PlaySFX(entityView.transform, entityView.SfxProfile.CollisionResource);
                })
                .AddTo(disposables);
        }
    }
}
