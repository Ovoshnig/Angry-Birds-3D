using AngryBirds3D.Bird.Power;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Bird.ExplosionParticle;

public class ExplosionParticleViewBirdExploderMediator(ExplosionParticleView explosionParticleView,
    BirdExploder birdExploder) : Mediator
{
    private readonly ExplosionParticleView _explosionParticleView = explosionParticleView;
    private readonly BirdExploder _birdExploder = birdExploder;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdExploder.Exploded
            .Subscribe(OnActivated)
            .AddTo(disposables);
    }

    private void OnActivated(BirdExplosionData data) => _explosionParticleView.Play(data.Transform.position, data.Radius);
}
