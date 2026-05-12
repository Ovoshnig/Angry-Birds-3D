using R3;

public class SlingshotShooterBirdDestroyerMediator : Mediator
{
    private readonly SlingshotShooter _slingshotShooter;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly BirdQueue _birdQueue;
    private readonly PigTracker _pigTracker;

    public SlingshotShooterBirdDestroyerMediator(SlingshotShooter slingshotShooter,
        BirdDestroyer birdDestroyer,
        BirdQueue birdQueue,
        PigTracker pigTracker)
    {
        _slingshotShooter = slingshotShooter;
        _birdDestroyer = birdDestroyer;
        _birdQueue = birdQueue;
        _pigTracker = pigTracker;
    }

    public override void Start()
    {
        _birdDestroyer.Destroyed
            .Subscribe(_ => OnDestroyed())
            .AddTo(Disposables);
    }

    private void OnDestroyed()
    {
        if (_pigTracker.PigCount.CurrentValue == 0)
            return;

        if (_birdQueue.TryDequeueBird(out BirdEntityView birdEntityView))
            _slingshotShooter.SetCurrentBird(birdEntityView.FlyerView.Rigidbody);
    }
}
