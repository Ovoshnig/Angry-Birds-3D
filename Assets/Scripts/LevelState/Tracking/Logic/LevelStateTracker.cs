using R3;

public class LevelStateTracker
{
    private readonly BirdDestroyer _birdDestroyer;
    private readonly PigTracker _pigTracker;

    public LevelStateTracker(BirdDestroyer birdDestroyer, PigTracker pigTracker)
    {
        _birdDestroyer = birdDestroyer;
        _pigTracker = pigTracker;

        Completed = _birdDestroyer.Destroyed
            .Where(_ => _pigTracker.PigCount.CurrentValue == 0)
            .Select(_ => Unit.Default)
            .Share();
    }

    public Observable<Unit> Completed { get; }
}
