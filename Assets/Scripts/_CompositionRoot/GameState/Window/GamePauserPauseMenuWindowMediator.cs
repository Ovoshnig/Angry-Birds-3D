using R3;

public class GamePauserPauseMenuWindowMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly PauseMenuWindow _pauseMenuWindow;

    public GamePauserPauseMenuWindowMediator(GamePauser gamePauser, PauseMenuWindow pauseMenuWindow)
    {
        _gamePauser = gamePauser;
        _pauseMenuWindow = pauseMenuWindow;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _pauseMenuWindow.IsOpen
            .Subscribe(_gamePauser.SetPause)
            .AddTo(disposables);
    }
}
