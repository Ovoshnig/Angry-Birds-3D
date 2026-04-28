using R3;

public class SFXPlayerPoolBirdDestroyerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly BirdDestroyer _birdDestroyer;

    public SFXPlayerPoolBirdDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
        BirdDestroyer birdDestroyer)
    {
        _playerObjectPool = playerObjectPool;
        _birdDestroyer = birdDestroyer;
    }

    public override void Initialize()
    {
        _birdDestroyer.Destroyed
            .Subscribe(entityView =>
            _playerObjectPool.PlaySFX(entityView.transform, entityView.SFXSettings.DestructionResource))
            .AddTo(CompositeDisposable);
    }
}
