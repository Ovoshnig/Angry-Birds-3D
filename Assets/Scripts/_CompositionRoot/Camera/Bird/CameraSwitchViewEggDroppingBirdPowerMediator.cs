using Cysharp.Threading.Tasks;
using R3;

public class CameraSwitchViewEggDroppingBirdPowerMediator : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView;
    private readonly EggDroppingBirdPower _eggDroppingBirdPower;

    public CameraSwitchViewEggDroppingBirdPowerMediator(CameraSwitchView cameraSwitchView,
        EggDroppingBirdPower eggDroppingBirdPower)
    {
        _cameraSwitchView = cameraSwitchView;
        _eggDroppingBirdPower = eggDroppingBirdPower;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _eggDroppingBirdPower.EggDropped
            .Subscribe(_ => _cameraSwitchView.SwitchToStructureAsync().Forget())
            .AddTo(disposables);
    }
}
