using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Slingshot.Shooting;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition
{
    public class BirdFlyerSlingshotShooterMediator : Mediator
    {
        private readonly BirdFlyer _birdFlyer;
        private readonly SlingshotShooter _slingshotShooter;
    
        public BirdFlyerSlingshotShooterMediator(BirdFlyer birdFlyer,
            SlingshotShooter slingshotShooter)
        {
            _birdFlyer = birdFlyer;
            _slingshotShooter = slingshotShooter;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _slingshotShooter.Shot
                .Subscribe(birdRigidbody => _birdFlyer.StartFlight(birdRigidbody.GetComponent<BirdEntityView>()))
                .AddTo(disposables);
        }
    }
}
