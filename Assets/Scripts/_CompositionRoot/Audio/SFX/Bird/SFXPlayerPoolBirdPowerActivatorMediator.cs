using AngryBirds3D.Bird.Power;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolBirdPowerActivatorMediator(SFXPlayerObjectPool sFXPlayerObjectPool,
    BirdPowerActivator birdPowerActivator) : Mediator
{
    private readonly SFXPlayerObjectPool _sFXPlayerObjectPool = sFXPlayerObjectPool;
    private readonly BirdPowerActivator _birdPowerActivator = birdPowerActivator;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPowerActivator.Activated
            .Subscribe(birdEntityView => _sFXPlayerObjectPool.PlaySFX(birdEntityView.transform,
                birdEntityView.SfxProfile.PowerActivationResource))
            .AddTo(disposables);
    }
}
