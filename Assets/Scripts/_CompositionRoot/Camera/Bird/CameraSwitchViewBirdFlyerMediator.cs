using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Bird.Power;
using AngryBirds3D.Camera.Switching;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
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
        _birdFlyer.BirdCollided
            .Where(birdEntityView => birdEntityView.PowerView.PowerType != BirdPowerType.EggDropping)
            .Subscribe(_ => _cameraSwitchView.SwitchToStructureAsync().Forget())
            .AddTo(disposables);
    }
}
