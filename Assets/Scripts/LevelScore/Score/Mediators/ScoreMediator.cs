using R3;

public class ScoreMediator : UIMediator<ScoreView>
{
    private readonly ScoreModel _scoreModel;

    public ScoreMediator(ScoreModel scoreModel, ScoreView view) : base(view) =>
        _scoreModel = scoreModel;

    protected override void OnViewEnabled(ScoreView view, CompositeDisposable viewDisposables)
    {
        _scoreModel.Score
            .Subscribe(view.SetScoreSmoothly)
            .AddTo(viewDisposables);
    }
}
