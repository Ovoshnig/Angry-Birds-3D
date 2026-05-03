using R3;

public class FinalScoreViewLevelTrackerMediator : Mediator
{
    private readonly FinalScoreView _finalScoreView;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly ScoreModel _scoreModel;

    public FinalScoreViewLevelTrackerMediator(FinalScoreView finalScoreView,
        LevelStateTracker levelStateTracker,
        ScoreModel scoreModel)
    {
        _finalScoreView = finalScoreView;
        _levelStateTracker = levelStateTracker;
        _scoreModel = scoreModel;
    }

    public override void Start()
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _finalScoreView.SetScore(_scoreModel.Score.CurrentValue))
            .AddTo(Disposables);
    }
}
