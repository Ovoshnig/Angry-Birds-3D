using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Audio.SFX.Playing;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition
{
    public class SFXPlayerPoolClearingPanelViewMediator : Mediator
    {
        private readonly SFXPlayerObjectPool _sfxPlayer;
        private readonly ClearingPanelView _clearingPanelView;
        private readonly LevelSfxProfile _levelSfxProfile;
    
        public SFXPlayerPoolClearingPanelViewMediator(SFXPlayerObjectPool sfxPlayer,
            ClearingPanelView clearingPanelView,
            LevelSfxProfile levelSfxProfile)
        {
            _sfxPlayer = sfxPlayer;
            _clearingPanelView = clearingPanelView;
            _levelSfxProfile = levelSfxProfile;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _clearingPanelView.Shown
                .Subscribe(_ => _sfxPlayer.PlaySFX(_levelSfxProfile.ClearingPanelResource))
                .AddTo(disposables);
        }
    }
}
