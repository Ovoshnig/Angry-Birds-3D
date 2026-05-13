using R3;

public class PointsPoolBirdDisplayerMediator : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool;
    private readonly BirdPointsDisplayer _birdPointsDisplayer;

    public PointsPoolBirdDisplayerMediator(PointsObjectPool pointsObjectPool,
        BirdPointsDisplayer birdPointsDisplayer)
    {
        _pointsObjectPool = pointsObjectPool;
        _birdPointsDisplayer = birdPointsDisplayer;
    }

    public override void Start()
    {
        _birdPointsDisplayer.BirdDisplayStarted
            .Subscribe(@event => _pointsObjectPool.ShowPoints(@event.Position, @event.PointsSettings))
            .AddTo(Disposables);
    }
}
