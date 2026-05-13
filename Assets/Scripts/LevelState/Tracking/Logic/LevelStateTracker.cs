using R3;

public class LevelStateTracker
{
    public LevelStateTracker(BirdQueue birdQueue,
        BirdDestroyer birdDestroyer,
        PigTracker pigTracker,
        SlingshotShooter slingshotShooter)
    {
        Cleared = Observable.Merge(
            birdDestroyer.Destroyed
                .Where(_ => pigTracker.PigCount.CurrentValue == 0)
                .AsUnitObservable(),
            pigTracker.PigsLeft
                .Where(_ => slingshotShooter.CurrentState.CurrentValue != SlingshotShooter.SlingshotState.Idle)
                .AsUnitObservable())
            .Take(1)
            .Share();

        Failed = birdDestroyer.Destroyed
            .Where(_ => !birdQueue.Any
                && slingshotShooter.CurrentState.CurrentValue == SlingshotShooter.SlingshotState.Idle
                && pigTracker.PigCount.CurrentValue > 0)
            .AsUnitObservable()
            .Take(1)
            .Share();

        Completed = Observable.Merge(Cleared, Failed)
            .Take(1);
    }

    public Observable<Unit> Cleared { get; }
    public Observable<Unit> Failed { get; }
    public Observable<Unit> Completed { get; }
}
