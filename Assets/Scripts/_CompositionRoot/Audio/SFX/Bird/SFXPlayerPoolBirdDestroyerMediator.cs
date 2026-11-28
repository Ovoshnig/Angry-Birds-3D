using AngryBirds3D.Bird.Destruction;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolBirdDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
    BirdDestroyer birdDestroyer) : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool = playerObjectPool;
    private readonly BirdDestroyer _birdDestroyer = birdDestroyer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdDestroyer.Destroyed
            .Subscribe(entityView =>
                _playerObjectPool.PlaySFX(entityView.transform, entityView.SfxProfile.DestructionResource))
            .AddTo(disposables);
    }
}
