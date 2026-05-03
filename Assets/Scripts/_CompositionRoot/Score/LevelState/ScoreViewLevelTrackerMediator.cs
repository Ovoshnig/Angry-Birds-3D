using R3;

public class ScoreViewLevelTrackerMediator : Mediator
{
    private readonly ScoreView _scoreView;
    private readonly LevelStateTracker _levelStateTracker;

    public ScoreViewLevelTrackerMediator(ScoreView scoreView, LevelStateTracker levelStateTracker)
    {
        _scoreView = scoreView;
        _levelStateTracker = levelStateTracker;
    }

    public override void Start()
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _scoreView.gameObject.SetActive(false))
            .AddTo(Disposables);
    }
}
