using R3;
using System;

public class LevelStateTracker
{
    private readonly PigTracker _pigTracker;
    private readonly BirdSettings _birdSettings;

    public LevelStateTracker(PigTracker pigTracker, BirdSettings birdSettings)
    {
        _pigTracker = pigTracker;
        _birdSettings = birdSettings;

        Completed = _pigTracker.PigsLeft
            .Delay(TimeSpan.FromSeconds(_birdSettings.DestructionDelay), UnityTimeProvider.Update);
    }

    public Observable<Unit> Completed { get; }
}
