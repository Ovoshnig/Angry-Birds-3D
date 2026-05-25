using R3;

public class PauseWindowLevelTrackerMediator : Mediator
{
    private readonly PauseMenuWindow _pauseMenuWindow;
    private readonly LevelStateTracker _levelStateTracker;

    public PauseWindowLevelTrackerMediator(PauseMenuWindow pauseMenuWindow, LevelStateTracker levelStateTracker)
    {
        _pauseMenuWindow = pauseMenuWindow;
        _levelStateTracker = levelStateTracker;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _pauseMenuWindow.StopSwitching())
            .AddTo(disposables);
    }
}
