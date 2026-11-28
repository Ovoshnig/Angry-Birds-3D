using Ovoshnig.GameState.Pause;
using Ovoshnig.Mediation;
using Ovoshnig.Window.Window;
using R3;

namespace AngryBirds3D.Composition;

public class GamePauserPauseMenuWindowMediator(GamePauser gamePauser, PauseMenuWindow pauseMenuWindow)
    : Mediator
{
    private readonly GamePauser _gamePauser = gamePauser;
    private readonly PauseMenuWindow _pauseMenuWindow = pauseMenuWindow;

    protected override void Bind(CompositeDisposable disposables)
    {
        _pauseMenuWindow.IsOpen
            .Subscribe(_gamePauser.SetPause)
            .AddTo(disposables);
    }
}
