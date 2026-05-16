using Cysharp.Threading.Tasks;
using R3;

public class CameraSwitchViewBirdFlyerMediator : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly BirdFlyer _birdFlyer;

    public CameraSwitchViewBirdFlyerMediator(CameraSwitchView cameraSwitchView, BirdFlyer birdFlyer)
    {
        _cameraSwitchView = cameraSwitchView;
        _birdFlyer = birdFlyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdFlyer.FlightInterrupted
            .Subscribe(_ => _cameraSwitchView.SwitchToStructureAsync().Forget())
            .AddTo(disposables);
    }
}
