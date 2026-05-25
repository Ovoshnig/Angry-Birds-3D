using R3;

public class LevelTrackerPauseButtonViewMediator : UIMediator<PauseButtonView>
{
    private readonly LevelStateTracker _levelStateTracker;

    public LevelTrackerPauseButtonViewMediator(LevelStateTracker levelStateTracker, PauseButtonView view)
        : base(view) => _levelStateTracker = levelStateTracker;

    protected override void OnViewEnabled(PauseButtonView view, CompositeDisposable viewDisposables)
    {
        _levelStateTracker.Completed
            .Subscribe(_ => view.gameObject.SetActive(false))
            .AddTo(viewDisposables);
    }
}
