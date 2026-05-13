using R3;

public class LevelStateTracker
{
    public LevelStateTracker(BirdDestroyer birdDestroyer, PigTracker pigTracker,
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
    }

    public Observable<Unit> Cleared { get; }
}
