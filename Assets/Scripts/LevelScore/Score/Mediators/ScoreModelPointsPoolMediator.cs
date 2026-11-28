using AngryBirds3D.LevelScore.Points;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.LevelScore.Score;

public class ScoreModelPointsPoolMediator(ScoreModel scoreModel, PointsObjectPool pointsPool) : Mediator
{
    private readonly ScoreModel _scoreModel = scoreModel;
    private readonly PointsObjectPool _pointsPool = pointsPool;

    protected override void Bind(CompositeDisposable disposables)
    {
        _pointsPool.PointsAdded
            .Subscribe(_scoreModel.Increase)
            .AddTo(disposables);
    }
}
