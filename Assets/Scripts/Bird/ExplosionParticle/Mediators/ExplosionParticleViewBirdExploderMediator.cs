using R3;

public class ExplosionParticleViewBirdExploderMediator : Mediator
{
    private readonly ExplosionParticleView _explosionParticleView;
    private readonly BirdExploder _birdExploder;

    public ExplosionParticleViewBirdExploderMediator(ExplosionParticleView explosionParticleView,
        BirdExploder birdExploder)
    {
        _explosionParticleView = explosionParticleView;
        _birdExploder = birdExploder;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdExploder.Exploded
            .Subscribe(OnActivated)
            .AddTo(disposables);
    }

    private void OnActivated(BirdExplosionData data) => _explosionParticleView.Play(data.Transform.position, data.Radius);
}
