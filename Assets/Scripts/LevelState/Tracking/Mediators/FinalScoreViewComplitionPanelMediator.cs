using R3;

public class FinalScoreViewComplitionPanelMediator : Mediator
{
    private readonly FinalScoreView _finalScoreView;
    private readonly ClearingPanelView _clearingPanelView;
    private readonly ScoreModel _scoreModel;

    public FinalScoreViewComplitionPanelMediator(FinalScoreView finalScoreView,
        ClearingPanelView clearingPanelView,
        ScoreModel scoreModel)
    {
        _finalScoreView = finalScoreView;
        _clearingPanelView = clearingPanelView;
        _scoreModel = scoreModel;
    }

    public override void Start()
    {
        _clearingPanelView.Shown
            .Subscribe(_ => _finalScoreView.SetScore(_scoreModel.Score.CurrentValue))
            .AddTo(Disposables);
    }
}
