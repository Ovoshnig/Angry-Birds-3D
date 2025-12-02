using R3;

public class LevelStateBirdFlyerMediator : Mediator
{
    private readonly LevelStateLogic _levelStateLogic;
    private readonly BirdFlyer _birdFlyer;

    public LevelStateBirdFlyerMediator(LevelStateLogic levelStateLogic,
        BirdFlyer birdFlyer)
    {
        _levelStateLogic = levelStateLogic;
        _birdFlyer = birdFlyer;
    }

    public override void Initialize()
    {
        _birdFlyer.BirdCollided
            .Subscribe(_ => _levelStateLogic.SetState(LevelState.Idle))
            .AddTo(CompositeDisposable);
    }
}

