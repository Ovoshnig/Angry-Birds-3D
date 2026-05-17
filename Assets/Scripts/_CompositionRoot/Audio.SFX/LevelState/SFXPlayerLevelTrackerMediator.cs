using R3;

public class SFXPlayerLevelTrackerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _sfxPlayer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly LevelSFXSettings _levelSFXSettings;

    public SFXPlayerLevelTrackerMediator(SFXPlayerObjectPool sfxPlayer,
        LevelStateTracker levelStateTracker,
        LevelSFXSettings levelSFXSettings)
    {
        _sfxPlayer = sfxPlayer;
        _levelStateTracker = levelStateTracker;
        _levelSFXSettings = levelSFXSettings;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Started
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSFXSettings.StartResource))
            .AddTo(disposables);

        _levelStateTracker.MovedToNext
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSFXSettings.NextResource))
            .AddTo(disposables);

        _levelStateTracker.Cleared
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSFXSettings.ClearingResource))
            .AddTo(disposables);

        _levelStateTracker.Failed
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSFXSettings.FailureResource))
            .AddTo(disposables);
    }
}
