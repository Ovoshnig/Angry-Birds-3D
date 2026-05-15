using R3;

public class GameQuitterMediator : UIMediator<GameQuitterButtonView>
{
    private readonly GameQuitter _gameQuitter;

    public GameQuitterMediator(GameQuitter gameQuitter, GameQuitterButtonView view)
        : base(view) => _gameQuitter = gameQuitter;

    protected override void OnViewEnabled(GameQuitterButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _gameQuitter.Quit())
            .AddTo(viewDisposables);
    }
}
