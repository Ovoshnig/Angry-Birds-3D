using R3;

public class FeatherParticleViewBirdDestroyerMediator : Mediator
{
    private readonly FeatherParticleView _featherParticleView;
    private readonly BirdDestroyer _birdDestroyer;

    public FeatherParticleViewBirdDestroyerMediator(FeatherParticleView featherParticleView,
        BirdDestroyer birdDestroyer)
    {
        _featherParticleView = featherParticleView;
        _birdDestroyer = birdDestroyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(disposables);
    }

    private void OnDestroyed(BirdEntityView birdEntityView) =>
        _featherParticleView.EmitMax(birdEntityView.transform.position, birdEntityView.FeatherColor);
}
