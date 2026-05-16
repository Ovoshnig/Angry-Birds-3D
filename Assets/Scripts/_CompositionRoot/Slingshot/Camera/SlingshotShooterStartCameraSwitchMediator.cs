using R3;

public class SlingshotShooterStartCameraSwitchMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly StartCameraSwitch _startCameraSwitch;
    private readonly BirdQueue _birdQueue;

    public SlingshotShooterStartCameraSwitchMediator(SlingshotShooter slingshotShooter,
        StartCameraSwitch startCameraSwitch,
        BirdQueue birdQueue)
    {
        _slingshotShooter = slingshotShooter;
        _startCameraSwitch = startCameraSwitch;
        _birdQueue = birdQueue;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _startCameraSwitch.Completed
            .Subscribe(_ =>
            {
                _birdQueue.TryDequeueBird(out BirdEntityView birdEntityView);
                _slingshotShooter.SetCurrentBird(birdEntityView.FlyerView.Rigidbody);
            })
            .AddTo(disposables);
    }
}
