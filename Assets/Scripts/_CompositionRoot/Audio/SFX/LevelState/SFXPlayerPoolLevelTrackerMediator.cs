using R3;

public class SFXPlayerPoolLevelTrackerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _sfxPlayer;
    private readonly LevelStateTracker _levelStateTracker;
    private readonly LevelSfxProfile _levelSfxProfile;

    public SFXPlayerPoolLevelTrackerMediator(SFXPlayerObjectPool sfxPlayer,
        LevelStateTracker levelStateTracker,
        LevelSfxProfile levelSfxProfile)
    {
        _sfxPlayer = sfxPlayer;
        _levelStateTracker = levelStateTracker;
        _levelSfxProfile = levelSfxProfile;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _levelStateTracker.Started
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.StartResource))
            .AddTo(disposables);

        _levelStateTracker.MovedToNext
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.NextResource))
            .AddTo(disposables);

        _levelStateTracker.Cleared
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.ClearingResource))
            .AddTo(disposables);

        _levelStateTracker.Failed
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.FailureResource))
            .AddTo(disposables);
    }
}
