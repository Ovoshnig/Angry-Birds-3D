using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class SFXPlayerPoolClearingPanelViewMediator(SFXPlayerObjectPool sfxPlayer,
    ClearingPanelView clearingPanelView, LevelSfxProfile levelSfxProfile) : Mediator
{
    private readonly SFXPlayerObjectPool _sfxPlayer = sfxPlayer;
    private readonly ClearingPanelView _clearingPanelView = clearingPanelView;
    private readonly LevelSfxProfile _levelSfxProfile = levelSfxProfile;

    protected override void Bind(CompositeDisposable disposables)
    {
        _clearingPanelView.Shown
            .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.ClearingPanelResource))
            .AddTo(disposables);
    }
}
