using Ovoshnig.GameState.Pause;
using Ovoshnig.Mediation;
using Ovoshnig.Scene.Switching;
using R3;

namespace AngryBirds3D.Composition;

public class GamePauserSceneSwitchMediator(GamePauser gamePauser, SceneSwitch sceneSwitch) : Mediator
{
    private readonly GamePauser _gamePauser = gamePauser;
    private readonly SceneSwitch _sceneSwitch = sceneSwitch;

    protected override void Bind(CompositeDisposable disposables)
    {
        _sceneSwitch.IsSceneLoading
            .Subscribe(_gamePauser.SetPause)
            .AddTo(disposables);
    }
}
