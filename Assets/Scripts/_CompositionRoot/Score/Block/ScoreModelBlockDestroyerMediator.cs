using R3;

public class ScoreModelBlockDestroyerMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly BlockDestroyer _blockDestroyer;

    public ScoreModelBlockDestroyerMediator(ScoreModel scoreModel,
        BlockDestroyer blockDestroyer)
    {
        _scoreModel = scoreModel;
        _blockDestroyer = blockDestroyer;
    }

    public override void Initialize()
    {
        _blockDestroyer.Destroyed
            .Subscribe(destructionEvent => _scoreModel.Increase(destructionEvent.Points))
            .AddTo(CompositeDisposable);
    }
}
