using AngryBirds3D.Camera.Switching;
using Ovoshnig.Mediation;
using Ovoshnig.ObjectCollision.Collision;
using R3;

namespace AngryBirds3D.Composition;

public class ObjectColliderStartCameraSwitchMediator(ObjectCollider objectCollider,
    StartCameraSwitch startCameraSwitch) : Mediator
{
    private readonly ObjectCollider _objectCollider = objectCollider;
    private readonly StartCameraSwitch _startCameraSwitch = startCameraSwitch;

    protected override void Bind(CompositeDisposable disposables)
    {
        _startCameraSwitch.Completed
            .Subscribe(_ => _objectCollider.Subscribe())
            .AddTo(disposables);
    }
}
