using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.UI.Basic;
using Ovoshnig.Window.Pause;
using R3;

namespace AngryBirds3D.Composition;

public class LevelTrackerPauseButtonViewMediator(LevelStateTracker levelStateTracker, PauseButtonView view)
    : UIViewMediator<PauseButtonView>(view)
{
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;

    protected override void OnViewEnabled(PauseButtonView view, CompositeDisposable viewDisposables)
    {
        _levelStateTracker.Completed
            .Subscribe(_ => view.gameObject.SetActive(false))
            .AddTo(viewDisposables);
    }
}
