using R3;

public class ScoreViewCompletionPanelsMediator : Mediator
{
    private readonly ScoreView _scoreView;
    private readonly ClearingPanelView _clearingPanelView;
    private readonly FailurePanelView _failurePanelView;

    public ScoreViewCompletionPanelsMediator(ScoreView scoreView,
        ClearingPanelView clearingPanelView,
        FailurePanelView failurePanelView)
    {
        _scoreView = scoreView;
        _clearingPanelView = clearingPanelView;
        _failurePanelView = failurePanelView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        Observable.Merge(_clearingPanelView.Shown, _failurePanelView.Shown)
            .Subscribe(_ => _scoreView.gameObject.SetActive(false))
            .AddTo(disposables);
    }
}
