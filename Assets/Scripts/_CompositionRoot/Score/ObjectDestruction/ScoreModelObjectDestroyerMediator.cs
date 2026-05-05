using R3;

public class ScoreModelObjectDestroyerMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly ObjectDestroyer _destroyer;

    public ScoreModelObjectDestroyerMediator(ScoreModel scoreModel, ObjectDestroyer destroyer)
    {
        _scoreModel = scoreModel;
        _destroyer = destroyer;
    }

    public override void Start()
    {
        _destroyer.Destroyed
            .Subscribe(destructionEvent =>
                _scoreModel.Increase(destructionEvent.DestroyerView.Settings.PointsSettings.Points))
            .AddTo(Disposables);
    }
}
