using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Slingshot.Shooting;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class BirdFlyerSlingshotShooterMediator(BirdFlyer birdFlyer, SlingshotShooter slingshotShooter)
    : Mediator
{
    private readonly BirdFlyer _birdFlyer = birdFlyer;
    private readonly SlingshotShooter _slingshotShooter = slingshotShooter;

    protected override void Bind(CompositeDisposable disposables)
    {
        _slingshotShooter.Shot
            .Subscribe(birdRigidbody => _birdFlyer.StartFlight(birdRigidbody.GetComponent<BirdEntityView>()))
        .AddTo(disposables);
    }
}
