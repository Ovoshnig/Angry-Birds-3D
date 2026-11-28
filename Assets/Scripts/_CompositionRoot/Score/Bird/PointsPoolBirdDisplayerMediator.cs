using AngryBirds3D.Bird.Points;
using AngryBirds3D.LevelScore.Points;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class PointsPoolBirdDisplayerMediator(PointsObjectPool pointsObjectPool,
    BirdPointsDisplayer birdPointsDisplayer) : Mediator
{
    private readonly PointsObjectPool _pointsObjectPool = pointsObjectPool;
    private readonly BirdPointsDisplayer _birdPointsDisplayer = birdPointsDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPointsDisplayer.PointsDisplayStarted
            .Subscribe(data => _pointsObjectPool.ShowPoints(data.Position, data.PointsSettings))
            .AddTo(disposables);
    }
}
