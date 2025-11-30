using R3;

public class SlingshotShooterBirdQueueMediator : Mediator
{
    private readonly SlingshotShooterView _slingshotShooterView;
    private readonly BirdQueue _birdQueue;

    public SlingshotShooterBirdQueueMediator(SlingshotShooterView slingshotShooterView,
        BirdQueue birdQueue)
    {
        _slingshotShooterView = slingshotShooterView;
        _birdQueue = birdQueue;
    }

    public override void Initialize()
    {
        _birdQueue.BirdDequeued
            .Subscribe(birdFlyerView => _slingshotShooterView.SetCurrentBird(birdFlyerView))
            .AddTo(CompositeDisposable);
    }
}
