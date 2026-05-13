using R3;

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

    public override void Start()
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _slingshotShooter.StopShooting())
            .AddTo(Disposables);
    }
}
