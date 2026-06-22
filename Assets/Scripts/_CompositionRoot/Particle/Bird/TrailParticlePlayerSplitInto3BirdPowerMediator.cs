using R3;

public class TrailParticlePlayerSplitInto3BirdPowerMediator : Mediator
{
    private readonly TrailParticlePlayer _trailParticlePlayer;
    private readonly SplitInto3BirdPower _splitInto3Power;

    public TrailParticlePlayerSplitInto3BirdPowerMediator(TrailParticlePlayer trailParticlePlayer,
        SplitInto3BirdPower splitInto3Power)
    {
        _trailParticlePlayer = trailParticlePlayer;
        _splitInto3Power = splitInto3Power;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _splitInto3Power.CloneCreated
            .Subscribe(clone => _trailParticlePlayer.StartPlaying(clone.transform))
            .AddTo(disposables);
    }
}
