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

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPointsDisplayer.PointsDisplayStarted
            .Subscribe(data => _pointsObjectPool.ShowPoints(data.Position, data.PointsSettings))
            .AddTo(disposables);
    }
}
