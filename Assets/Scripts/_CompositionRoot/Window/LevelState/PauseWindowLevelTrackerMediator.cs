using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Mediation;
using Ovoshnig.Window.Window;
using R3;

namespace AngryBirds3D.Composition;

public class PauseWindowLevelTrackerMediator(PauseMenuWindow pauseMenuWindow,
    LevelStateTracker levelStateTracker) : Mediator
{
    private readonly PauseMenuWindow _pauseMenuWindow = pauseMenuWindow;
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _pauseMenuWindow.StopToggling())
            .AddTo(disposables);
    }
}
