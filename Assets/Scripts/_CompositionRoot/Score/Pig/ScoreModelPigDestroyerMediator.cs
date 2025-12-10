using R3;

public class ScoreModelPigDestroyerMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly PigDestroyer _pigDestroyer;

    public ScoreModelPigDestroyerMediator(ScoreModel scoreModel,
        PigDestroyer pigDestroyer)
    {
        _scoreModel = scoreModel;
        _pigDestroyer = pigDestroyer;
    }

    public override void Initialize()
    {
        _pigDestroyer.Destroyed
            .Subscribe(destructionEvent => _scoreModel.Increase(destructionEvent.PointsSettings.Points))
            .AddTo(CompositeDisposable);
    }
}
