using R3;

public class SFXPlayerPoolBirdExploderMediator : Mediator
{
    private readonly SFXPlayerObjectPool _sFXPlayerObjectPool;
    private readonly BirdExploder _birdExploder;

    public SFXPlayerPoolBirdExploderMediator(SFXPlayerObjectPool sFXPlayerObjectPool, BirdExploder birdExploder)
    {
        _sFXPlayerObjectPool = sFXPlayerObjectPool;
        _birdExploder = birdExploder;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdExploder.Exploded
            .Subscribe(data => _sFXPlayerObjectPool.PlaySFX(data.Transform, data.AudioResource))
            .AddTo(disposables);
    }
}
