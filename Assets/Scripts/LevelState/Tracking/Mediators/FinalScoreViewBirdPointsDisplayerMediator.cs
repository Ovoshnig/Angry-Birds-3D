using R3;

public class FinalScoreViewBirdPointsDisplayerMediator : Mediator
{
    private readonly FinalScoreView _finalScoreView;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;
    private readonly ScoreModel _scoreModel;

    public FinalScoreViewBirdPointsDisplayerMediator(FinalScoreView finalScoreView,
        BirdPointsDisplayer birdPointsDisplayer,
        ScoreModel scoreModel)
    {
        _finalScoreView = finalScoreView;
        _birdPointsDisplayer = birdPointsDisplayer;
        _scoreModel = scoreModel;
    }

    public override void Start()
    {
        _birdPointsDisplayer.BirdSequenceDisplayCompleted
            .Subscribe(_ => _finalScoreView.SetScore(_scoreModel.Score.CurrentValue))
            .AddTo(Disposables);
    }
}
