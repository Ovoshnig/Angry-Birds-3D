using R3;

public class GamePauserGameOverMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly GameOver _gameOver;

    public GamePauserGameOverMediator(GamePauser gamePauser,
        GameOver gameOver)
    {
        _gamePauser = gamePauser;
        _gameOver = gameOver;
    }

    public override void Initialize()
    {
        _gameOver.IsOver
            .Subscribe(OnGameOver)
            .AddTo(CompositeDisposable);
    }

    private void OnGameOver(bool isOver)
    {
        if (isOver)
            _gamePauser.Pause();
        else
            _gamePauser.UnPause();
    }
}
