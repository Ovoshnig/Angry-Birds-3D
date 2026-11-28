using AngryBirds3D.Bird.Power;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolBirdExploderMediator(SFXPlayerObjectPool sFXPlayerObjectPool,
    BirdExploder birdExploder) : Mediator
{
    private readonly SFXPlayerObjectPool _sFXPlayerObjectPool = sFXPlayerObjectPool;
    private readonly BirdExploder _birdExploder = birdExploder;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdExploder.Exploded
            .Subscribe(data => _sFXPlayerObjectPool.PlaySFX(data.Transform, data.AudioResource))
            .AddTo(disposables);
    }
}
