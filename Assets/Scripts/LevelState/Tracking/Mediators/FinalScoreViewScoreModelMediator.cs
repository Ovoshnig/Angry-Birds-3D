public class FinalScoreViewScoreModelMediator : UIMediator<FinalScoreView>
{
    private readonly FinalScoreView _finalScoreView;
    private readonly ScoreModel _scoreModel;

    public FinalScoreViewScoreModelMediator(FinalScoreView finalScoreView, ScoreModel scoreModel)
        : base(finalScoreView)
    {
        _finalScoreView = finalScoreView;
        _scoreModel = scoreModel;
    }

    protected override void OnViewEnabled() => _finalScoreView.SetScore(_scoreModel.Score.CurrentValue);
}
