using R3;

public class ClearingViewBirdPointsDisplayerMediator : Mediator
{
    private readonly ClearingPanelView _clearingPanelView;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;

    public ClearingViewBirdPointsDisplayerMediator(ClearingPanelView clearingPanelView,
        BirdPointsDisplayer birdPointsDisplayer)
    {
        _clearingPanelView = clearingPanelView;
        _birdPointsDisplayer = birdPointsDisplayer;
    }

    public override void Start()
    {
        _birdPointsDisplayer.AllDisplayed
            .Subscribe(_ => _clearingPanelView.SetActive(true))
            .AddTo(Disposables);
    }
}
