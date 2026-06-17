using R3;

public class ObjectColliderStartCameraSwitchMediator : Mediator
{
    private readonly ObjectCollider _objectCollider;
    private readonly StartCameraSwitch _startCameraSwitch;

    public ObjectColliderStartCameraSwitchMediator(ObjectCollider objectCollider, StartCameraSwitch startCameraSwitch)
    {
        _objectCollider = objectCollider;
        _startCameraSwitch = startCameraSwitch;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _startCameraSwitch.Completed
            .Subscribe(_ => _objectCollider.Subscribe())
            .AddTo(disposables);
    }
}
