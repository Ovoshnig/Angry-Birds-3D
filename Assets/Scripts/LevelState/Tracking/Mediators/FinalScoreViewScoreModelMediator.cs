using R3;

public class FinalScoreViewScoreModelMediator : UIMediator<FinalScoreView>
{
    private readonly ScoreModel _scoreModel;

    public FinalScoreViewScoreModelMediator(FinalScoreView view, ScoreModel scoreModel)
        : base(view) => _scoreModel = scoreModel;

    protected override void OnViewEnabled(FinalScoreView view, CompositeDisposable viewDisposables) =>
        view.SetScore(_scoreModel.Score.CurrentValue);
}
