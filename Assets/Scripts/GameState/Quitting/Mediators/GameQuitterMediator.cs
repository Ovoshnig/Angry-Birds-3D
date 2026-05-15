using R3;

public class GameQuitterMediator : UIMediator<GameQuitterButtonView>
{
    private readonly GameQuitter _gameQuitter;
    private readonly GameQuitterButtonView _gameQuitterButtonView;

    public GameQuitterMediator(GameQuitter gameQuitter, GameQuitterButtonView gameQuitterButtonView)
        : base(gameQuitterButtonView)
    {
        _gameQuitter = gameQuitter;
        _gameQuitterButtonView = gameQuitterButtonView;
    }

    protected override void OnViewEnabled()
    {
        _gameQuitterButtonView.Clicked
            .Subscribe(_ => _gameQuitter.Quit())
            .AddTo(Disposables);
    }
}
