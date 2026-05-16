using R3;

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

