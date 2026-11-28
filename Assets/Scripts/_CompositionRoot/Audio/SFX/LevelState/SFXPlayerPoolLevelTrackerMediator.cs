using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolLevelTrackerMediator(SFXPlayerObjectPool sfxPlayer,
    LevelStateTracker levelStateTracker, LevelSfxProfile levelSfxProfile) : Mediator
{
    private readonly SFXPlayerObjectPool _sfxPlayer = sfxPlayer;
    private readonly LevelStateTracker _levelStateTracker = levelStateTracker;
    private readonly LevelSfxProfile _levelSfxProfile = levelSfxProfile;

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
