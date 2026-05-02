using R3;

public class CompletionViewLevelTrackerMediator : Mediator
{
    private readonly CompletionPanelView _completionPanelView;
    private readonly LevelStateTracker _levelStateTracker;

    public CompletionViewLevelTrackerMediator(CompletionPanelView completionPanelView, LevelStateTracker levelStateTracker)
    {
        _completionPanelView = completionPanelView;
        _levelStateTracker = levelStateTracker;
    }

    public override void Start()
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _completionPanelView.SetActive(true))
            .AddTo(Disposables);
    }
}
