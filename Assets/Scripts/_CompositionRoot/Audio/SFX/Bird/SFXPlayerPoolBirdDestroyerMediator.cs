using AngryBirds3D.Bird.Destruction;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
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

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdDestroyer.Destroyed
            .Subscribe(entityView =>
                _playerObjectPool.PlaySFX(entityView.transform, entityView.SfxProfile.DestructionResource))
            .AddTo(disposables);
    }
}
