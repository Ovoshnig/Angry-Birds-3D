using AngryBirds3D.Bird.Power;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Bird.Animation
{
    public class WhiteBirdAnimatorViewPowerActivatorMediator : Mediator
    {
        private readonly BirdPowerActivator _birdPowerActivator;

        public WhiteBirdAnimatorViewPowerActivatorMediator(BirdPowerActivator birdPowerActivator) =>
            _birdPowerActivator = birdPowerActivator;

        protected override void Bind(CompositeDisposable disposables)
        {
            _birdPowerActivator.Activated
                .Where(entityView => entityView.PowerView.PowerType == BirdPowerType.EggDropping)
                .Subscribe(entityView => entityView.AnimatorView.SetTrigger(BirdAnimationConstants.EggDroppedTriggerId))
                .AddTo(disposables);
        }
    }
}
