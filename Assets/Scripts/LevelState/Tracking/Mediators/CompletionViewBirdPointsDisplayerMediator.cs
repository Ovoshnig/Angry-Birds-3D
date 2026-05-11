using R3;

public class CompletionViewBirdPointsDisplayerMediator : Mediator
{
    private readonly CompletionPanelView _completionPanelView;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;

    public CompletionViewBirdPointsDisplayerMediator(CompletionPanelView completionPanelView,
        BirdPointsDisplayer birdPointsDisplayer)
    {
        _completionPanelView = completionPanelView;
        _birdPointsDisplayer = birdPointsDisplayer;
    }

    public override void Start()
    {
        _birdPointsDisplayer.AllDisplayed
            .Subscribe(_ => _completionPanelView.SetActive(true))
            .AddTo(Disposables);
    }
}
