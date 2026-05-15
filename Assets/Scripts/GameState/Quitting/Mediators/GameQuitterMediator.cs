using R3;

public class GameQuitterMediator : UIMediator<GameQuitterButtonView>
{
    private readonly GameQuitter _gameQuitter;
    private readonly GameQuitterButtonView _gameQuitButtonView;

    public GameQuitterMediator(GameQuitter gameQuitter, GameQuitterButtonView gameQuitButtonView)
        : base(gameQuitButtonView)
    {
        _gameQuitter = gameQuitter;
        _gameQuitButtonView = gameQuitButtonView;
    }

    protected override void OnViewEnabled()
    {
        _gameQuitButtonView.Clicked
            .Subscribe(_ => _gameQuitter.Quit())
            .AddTo(Disposables);
    }
}
