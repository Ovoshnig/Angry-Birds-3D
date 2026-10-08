using Ovoshnig.GameState.Pause;
using Ovoshnig.Mediation;
using Ovoshnig.Scene.Switching;
using R3;

namespace AngryBirds3D.Composition
{
    public class GamePauserSceneSwitchMediator : Mediator
    {
        private readonly GamePauser _gamePauser;
        private readonly SceneSwitch _sceneSwitch;
    
        public GamePauserSceneSwitchMediator(GamePauser gamePauser,
            SceneSwitch sceneSwitch)
        {
            _gamePauser = gamePauser;
            _sceneSwitch = sceneSwitch;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            _sceneSwitch.IsSceneLoading
                .Subscribe(_gamePauser.SetPause)
                .AddTo(disposables);
        }
    }
}
