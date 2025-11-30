using R3;

public class LevelStateSlingshotShooterMediator : Mediator
{
    private readonly LevelStateLogic _levelStateLogic;
    private readonly SlingshotShooterView _slingshotShooterView;

    public LevelStateSlingshotShooterMediator(LevelStateLogic levelStateLogic,
        SlingshotShooterView slingshotShooterView)
    {
        _levelStateLogic = levelStateLogic;
        _slingshotShooterView = slingshotShooterView;
    }

    public override void Initialize()
    {
        _slingshotShooterView.BirdCollided
            .Subscribe(_ => _levelStateLogic.SetState(LevelState.Idle))
            .AddTo(CompositeDisposable);
    }
}

