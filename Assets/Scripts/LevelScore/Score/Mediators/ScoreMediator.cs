using R3;

public class ScoreMediator : UIMediator<ScoreView>
{
    private readonly ScoreModel _scoreModel;
    private readonly ScoreView _scoreView;

    public ScoreMediator(ScoreModel scoreModel, ScoreView scoreView) : base(scoreView)
    {
        _scoreModel = scoreModel;
        _scoreView = scoreView;
    }

    protected override void OnViewEnabled()
    {
        _scoreModel.Score
            .Subscribe(_scoreView.SetScoreSmoothly)
            .AddTo(Disposables);
    }
}
