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

    public override void Start()
    {
        _slingshotShooter.Shot
            .Subscribe(birdRigidbody => _birdFlyer.StartFlight(birdRigidbody.GetComponent<BirdEntityView>()))
            .AddTo(Disposables);
    }
}

