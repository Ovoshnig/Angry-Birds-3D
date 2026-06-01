using R3;

public class ScoreModelFinalScoreViewMediator : UIViewMediator<FinalScoreView>
{
    private readonly ScoreModel _scoreModel;

    public ScoreModelFinalScoreViewMediator(ScoreModel scoreModel, FinalScoreView view)
        : base(view) => _scoreModel = scoreModel;

    protected override void OnViewEnabled(FinalScoreView view, CompositeDisposable viewDisposables) =>
        view.SetScore(_scoreModel.Score.CurrentValue);
}
