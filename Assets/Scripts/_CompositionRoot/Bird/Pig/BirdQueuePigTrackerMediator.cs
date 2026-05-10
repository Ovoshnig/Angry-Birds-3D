using R3;
using VContainer.Unity;

public class BirdQueuePigTrackerMediator : Mediator, IPostStartable
{
    private readonly BirdQueue _birdQueue;
    private readonly BirdDestroyer _birdDestroyer;
    private readonly PigTracker _pigTracker;

    public BirdQueuePigTrackerMediator(BirdQueue birdQueue,
        BirdDestroyer birdDestroyer,
        PigTracker pigTracker)
    {
        _birdQueue = birdQueue;
        _birdDestroyer = birdDestroyer;
        _pigTracker = pigTracker;
    }

    public override void Start()
    {
        _birdDestroyer.Destroyed
            .Subscribe(_ =>
            {
                if (_pigTracker.PigCount.CurrentValue > 0)
                    _birdQueue.TryDequeueBird();
            })
            .AddTo(Disposables);
    }

    public void PostStart() => _birdQueue.TryDequeueBird();
}
