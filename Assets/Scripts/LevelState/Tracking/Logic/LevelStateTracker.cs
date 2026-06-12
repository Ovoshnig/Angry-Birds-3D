using R3;
using System;
using VContainer.Unity;

public class LevelStateTracker : IPostStartable, IDisposable
{
    private readonly Subject<Unit> _started = new();

    public LevelStateTracker(StartCameraSwitch startCameraSwitch,
        BirdDestroyer birdDestroyer,
        BirdTracker birdTracker,
        PigTracker pigTracker)
    {
        MovedToNext = Observable.Merge(
            startCameraSwitch.Completed,
            birdDestroyer.Destroyed
                .Where(_ => pigTracker.AnyPigs && birdTracker.AnyBirds)
                .AsUnitObservable())
            .Share();

        Cleared = Observable.Merge(
            birdDestroyer.Destroyed
                .Where(_ => !pigTracker.AnyPigs)
                .AsUnitObservable(),
            pigTracker.PigsLeft
                .Where(_ => !birdTracker.IsBirdLaunched.CurrentValue))
            .Take(1)
            .Share();

        Failed = birdTracker.BirdsLeft
            .Where(_ => pigTracker.AnyPigs)
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
