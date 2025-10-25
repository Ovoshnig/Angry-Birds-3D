using R3;

public class ScoreMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly ScoreView _scoreView;

    public ScoreMediator(ScoreModel scoreModel, ScoreView scoreView)
    {
        _scoreModel = scoreModel;
        _scoreView = scoreView;
    }

    public override void Initialize()
    {
        _scoreModel.Score
            .Subscribe(_scoreView.SetScore)
            .AddTo(CompositeDisposable);
    }
}
