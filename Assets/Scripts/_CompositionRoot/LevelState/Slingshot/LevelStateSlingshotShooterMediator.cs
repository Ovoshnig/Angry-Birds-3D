using R3;

public class LevelStateSlingshotShooterMediator : Mediator
{
    private readonly LevelStateLogic _levelStateLogic;
    private readonly SlingshotShooter _slingshotShooter;

    public LevelStateSlingshotShooterMediator(LevelStateLogic levelStateLogic,
        SlingshotShooter slingshotShooter)
    {
        _levelStateLogic = levelStateLogic;
        _slingshotShooter = slingshotShooter;
    }

    public override void Initialize()
    {
        _slingshotShooter.BirdCollided
            .Subscribe(_ => _levelStateLogic.SetState(LevelState.Idle))
            .AddTo(CompositeDisposable);
    }
}

