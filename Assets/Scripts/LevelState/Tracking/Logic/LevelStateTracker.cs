using R3;

public class LevelStateTracker
{
    private readonly PigTracker _pigTracker;

    public LevelStateTracker(PigTracker pigTracker)
    {
        _pigTracker = pigTracker;

        Completed = _pigTracker.PigsLeft;
    }

    public Observable<Unit> Completed { get; }
}
