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

        Observable<Unit> clearedSource = Observable.Merge(
            birdDestroyer.Destroyed
                .Where(_ => !pigTracker.AnyPigs)
                .AsUnitObservable(),
            pigTracker.PigsLeft
                .Where(_ => !birdTracker.IsBirdLaunched.CurrentValue)
                .AsUnitObservable());

        Observable<Unit> failedSource = birdTracker.BirdsLeft
            .Where(_ => pigTracker.AnyPigs)
            .AsUnitObservable();

        Observable<bool> result = Observable.Merge(
                clearedSource.Select(_ => true),
                failedSource.Select(_ => false))
            .Take(1)
            .Share();

        Cleared = result
            .Where(isCleared => isCleared)
            .AsUnitObservable();

        Failed = result
            .Where(isCleared => !isCleared)
            .AsUnitObservable();

        Completed = result.AsUnitObservable();
    }

    public Observable<Unit> Started => _started;
    public Observable<Unit> MovedToNext { get; }
    public Observable<Unit> Cleared { get; }
    public Observable<Unit> Failed { get; }
    public Observable<Unit> Completed { get; }

    public void PostStart() => _started.OnNext(Unit.Default);

    public void Dispose() => _started.Dispose();
}
