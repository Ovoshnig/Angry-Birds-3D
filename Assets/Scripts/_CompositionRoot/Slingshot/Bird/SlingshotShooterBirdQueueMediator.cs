using R3;

public class SlingshotShooterBirdQueueMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly BirdQueue _birdQueue;

    public SlingshotShooterBirdQueueMediator(SlingshotShooter slingshotShooter,
        BirdQueue birdQueue)
    {
        _slingshotShooter = slingshotShooter;
        _birdQueue = birdQueue;
    }

    public override void Initialize()
    {
        _birdQueue.BirdDequeued
            .Subscribe(birdFlyerView => _slingshotShooter.SetCurrentBird(birdFlyerView))
            .AddTo(CompositeDisposable);
    }
}
