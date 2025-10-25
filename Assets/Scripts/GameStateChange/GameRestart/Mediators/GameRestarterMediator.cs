using R3;

public class GameRestarterMediator : Mediator
{
    private readonly GameRestarter _gameRestarter;
    private readonly GameRestarterView _gameRestarterView;

    public GameRestarterMediator(GameRestarter gameRestarter, 
        GameRestarterView gameRestarterView)
    {
        _gameRestarter = gameRestarter;
        _gameRestarterView = gameRestarterView;
    }

    public override void Initialize()
    {
        _gameRestarterView.Clicked
            .Subscribe(_ => _gameRestarter.Restart())
            .AddTo(CompositeDisposable);
    }
}
