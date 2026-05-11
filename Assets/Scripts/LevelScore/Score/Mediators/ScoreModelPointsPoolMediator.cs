using R3;

public class ScoreModelPointsPoolMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly PointsObjectPool _pointsPool;

    public ScoreModelPointsPoolMediator(ScoreModel scoreModel, PointsObjectPool pointsPool)
    {
        _scoreModel = scoreModel;
        _pointsPool = pointsPool;
    }

    public override void Start()
    {
        _pointsPool.PointsAdded
            .Subscribe(_scoreModel.Increase)
            .AddTo(Disposables);
    }
}
