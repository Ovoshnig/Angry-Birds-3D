using AngryBirds3D.LevelState.Tracking;
using AngryBirds3D.Slingshot.Shooting;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition
{
    public class SlingshotShooterLevelTrackerMediator : Mediator
    {
        private readonly SlingshotShooter _slingshotShooter;
        private readonly LevelStateTracker _levelStateTracker;
    
        public SlingshotShooterLevelTrackerMediator(SlingshotShooter slingshotShooter,
            LevelStateTracker levelStateTracker)
        {
            _slingshotShooter = slingshotShooter;
            _levelStateTracker = levelStateTracker;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _levelStateTracker.Completed
                .Subscribe(_ => _slingshotShooter.StopShooting())
                .AddTo(disposables);
        }
    }
}
