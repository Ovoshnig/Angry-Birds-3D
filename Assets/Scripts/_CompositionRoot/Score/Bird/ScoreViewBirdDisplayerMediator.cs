using R3;

public class ScoreViewBirdDisplayerMediator : Mediator
{
    private readonly ScoreView _scoreView;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;

    public ScoreViewBirdDisplayerMediator(ScoreView scoreView,
        BirdPointsDisplayer birdPointsDisplayer)
    {
        _scoreView = scoreView;
        _birdPointsDisplayer = birdPointsDisplayer;
    }

    public override void Start()
    {
        _birdPointsDisplayer.AllDisplayed
            .Subscribe(_ => _scoreView.gameObject.SetActive(false))
            .AddTo(Disposables);
    }
}
