using R3;

public class TrailParticlePlayerBirdPowerActivatorMediator : Mediator
{
    private readonly TrailParticlePlayer _trailParticlePlayer;
    private readonly BirdPowerActivator _birdPowerActivator;

    public TrailParticlePlayerBirdPowerActivatorMediator(TrailParticlePlayer trailParticlePlayer,
        BirdPowerActivator birdPowerActivator)
    {
        _trailParticlePlayer = trailParticlePlayer;
        _birdPowerActivator = birdPowerActivator;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdPowerActivator.Activated
            .Subscribe(_ => _trailParticlePlayer.PlayPowerParticle())
            .AddTo(disposables);
    }
}
