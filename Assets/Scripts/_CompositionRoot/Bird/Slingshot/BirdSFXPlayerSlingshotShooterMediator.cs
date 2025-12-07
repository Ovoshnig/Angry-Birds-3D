using R3;

public class BirdSFXPlayerSlingshotShooterMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;

    public BirdSFXPlayerSlingshotShooterMediator(SlingshotShooter slingshotShooter) =>
        _slingshotShooter = slingshotShooter;

    public override void Initialize()
    {
        _slingshotShooter.Shot
            .Subscribe(bird => bird.GetComponent<BirdSFXPlayerView>().PlayFlying())
            .AddTo(CompositeDisposable);
    }
}
