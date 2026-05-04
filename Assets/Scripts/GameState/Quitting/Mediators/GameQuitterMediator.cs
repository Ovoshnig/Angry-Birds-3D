using R3;

public class GameQuitterMediator : Mediator
{
    private readonly GameQuitter _gameQuitter;
    private readonly GameQuitButtonView _gameQuitButtonView;

    public GameQuitterMediator(GameQuitter gameQuitter, GameQuitButtonView gameQuitButtonView)
    {
        _gameQuitter = gameQuitter;
        _gameQuitButtonView = gameQuitButtonView;
    }

    public override void Start()
    {
        _gameQuitButtonView.Clicked
            .Subscribe(_ => _gameQuitter.Quit())
            .AddTo(Disposables);
    }
}
