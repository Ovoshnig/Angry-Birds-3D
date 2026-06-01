using R3;

public class ScoreModelViewMediator : UIViewMediator<ScoreView>
{
    private readonly ScoreModel _scoreModel;

    public ScoreModelViewMediator(ScoreModel scoreModel, ScoreView view) : base(view) =>
        _scoreModel = scoreModel;

    protected override void OnViewEnabled(ScoreView view, CompositeDisposable viewDisposables)
    {
        _scoreModel.Score
            .Subscribe(view.SetScoreSmoothly)
            .AddTo(viewDisposables);
    }
}
