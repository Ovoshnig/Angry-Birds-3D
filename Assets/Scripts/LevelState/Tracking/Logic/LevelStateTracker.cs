using R3;
using System;
using VContainer.Unity;

public class LevelStateTracker : IPostStartable, IDisposable
{
    private readonly Subject<Unit> _started = new();

    public LevelStateTracker(BirdQueue birdQueue,
        BirdDestroyer birdDestroyer,
        PigTracker pigTracker,
        SlingshotShooter slingshotShooter)
    {
        MovedToNext = birdDestroyer.Destroyed
            .Where(_ => pigTracker.PigCount.CurrentValue > 0
                && (birdQueue.Any || slingshotShooter.CurrentBird != null))
            .AsUnitObservable()
            .Share();

        Cleared = Observable.Merge(
            birdDestroyer.Destroyed
                .Where(_ => pigTracker.PigCount.CurrentValue == 0)
                .AsUnitObservable(),
            pigTracker.PigsLeft
                .Where(_ => slingshotShooter.CurrentBird != null)
                .AsUnitObservable())
            .Take(1)
            .Share();

        Failed = birdDestroyer.Destroyed
            .Where(_ => !birdQueue.Any && slingshotShooter.CurrentBird == null
                && pigTracker.PigCount.CurrentValue > 0)
            .AsUnitObservable()
            .Take(1)
            .Share();

        Completed = Observable.Merge(Cleared, Failed)
            .Take(1);
    }

    public Observable<Unit> Started => _started;
    public Observable<Unit> MovedToNext { get; }
    public Observable<Unit> Cleared { get; }
    public Observable<Unit> Failed { get; }
    public Observable<Unit> Completed { get; }

    public void PostStart() => _started.OnNext(Unit.Default);

    public void Dispose() => _started.Dispose();
}
