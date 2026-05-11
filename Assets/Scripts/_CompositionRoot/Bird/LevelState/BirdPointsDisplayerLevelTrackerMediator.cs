using Cysharp.Threading.Tasks;
using R3;

public class BirdPointsDisplayerLevelTrackerMediator : Mediator
{
    private readonly BirdPointsDisplayer _birdPointsDisplayer;
    private readonly LevelStateTracker _levelStateTracker;

    public BirdPointsDisplayerLevelTrackerMediator(BirdPointsDisplayer birdPointsDisplayer,
        LevelStateTracker levelStateTracker)
    {
        _birdPointsDisplayer = birdPointsDisplayer;
        _levelStateTracker = levelStateTracker;
    }

    public override void Start()
    {
        _levelStateTracker.Completed
            .Subscribe(_ => _birdPointsDisplayer.DisplayPointsAsync().Forget())
            .AddTo(Disposables);
    }
}
