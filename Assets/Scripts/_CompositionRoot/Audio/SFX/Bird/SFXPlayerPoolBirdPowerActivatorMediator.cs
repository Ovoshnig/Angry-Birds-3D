using AngryBirds3D.Bird.Power;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition
{
    public class SFXPlayerPoolBirdPowerActivatorMediator : Mediator
    {
        private readonly SFXPlayerObjectPool _sFXPlayerObjectPool;
        private readonly BirdPowerActivator _birdPowerActivator;
    
        public SFXPlayerPoolBirdPowerActivatorMediator(SFXPlayerObjectPool sFXPlayerObjectPool,
            BirdPowerActivator birdPowerActivator)
        {
            _sFXPlayerObjectPool = sFXPlayerObjectPool;
            _birdPowerActivator = birdPowerActivator;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _birdPowerActivator.Activated
                .Subscribe(birdEntityView =>
                    _sFXPlayerObjectPool.PlaySFX(birdEntityView.transform, birdEntityView.SfxProfile.PowerActivationResource))
                .AddTo(disposables);
        }
    }
}
