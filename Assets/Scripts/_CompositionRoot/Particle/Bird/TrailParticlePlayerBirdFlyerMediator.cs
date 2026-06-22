using Cysharp.Threading.Tasks;
using R3;

public class TrailParticlePlayerBirdFlyerMediator : Mediator
{
    private readonly TrailParticlePlayer _trailParticlePlayer;
    private readonly BirdFlyer _birdFlyer;

    public TrailParticlePlayerBirdFlyerMediator(TrailParticlePlayer trailParticlePlayer, BirdFlyer birdFlyer)
    {
        _trailParticlePlayer = trailParticlePlayer;
        _birdFlyer = birdFlyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdFlyer.FlightStarted
            .Subscribe(birdEntityView => _trailParticlePlayer.StartPlaying(birdEntityView.transform))
            .AddTo(disposables);

        _birdFlyer.FlightInterrupted
            .Subscribe(birdEntityView => _trailParticlePlayer.StopPlaying())
            .AddTo(disposables);
    }
}
