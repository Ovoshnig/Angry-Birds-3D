using R3;

public class GameOverMediator : Mediator
{
    private readonly GameOver _gameOver;
    private readonly GameOverView _gameOverView;

    public GameOverMediator(GameOver gameOver, GameOverView gameOverView)
    {
        _gameOver = gameOver;
        _gameOverView = gameOverView;
    }

    public override void Initialize()
    {
        _gameOver.IsOver
            .Subscribe(_gameOverView.SetActive)
            .AddTo(CompositeDisposable);
    }
}
