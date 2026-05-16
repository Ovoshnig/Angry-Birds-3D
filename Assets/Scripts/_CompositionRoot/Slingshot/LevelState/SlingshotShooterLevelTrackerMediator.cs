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

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _slingshotShooter.StopShooting())
            .AddTo(disposables);
    }
}
