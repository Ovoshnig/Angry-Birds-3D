using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Bird.Power;
using AngryBirds3D.Camera.Switching;
using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class CameraSwitchViewBirdFlyerMediator(CameraSwitchView cameraSwitchView, BirdFlyer birdFlyer)
    : Mediator
{
    private readonly CameraSwitchView _cameraSwitchView = cameraSwitchView;
    private readonly BirdFlyer _birdFlyer = birdFlyer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _birdFlyer.BirdCollided
            .Where(birdEntityView => birdEntityView.PowerView.PowerType != BirdPowerType.EggDropping)
            .Subscribe(_ => _cameraSwitchView.SwitchToStructureAsync().Forget())
            .AddTo(disposables);
    }
}
