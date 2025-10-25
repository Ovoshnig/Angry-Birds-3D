using R3;
using System;
using VContainer.Unity;

public class GameStateChanger : IInitializable, IDisposable
{
    private readonly GameRestarter _gameRestarter;
    private readonly Subject<Unit> _invadersDestroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public GameStateChanger(GameRestarter gameRestarter)
    {
        _gameRestarter = gameRestarter;

        GameRestarted = _gameRestarter.Restarted;
        GameStateChanged = Observable.Merge(InvadersDestroyed, GameRestarted);
    }

    public Observable<Unit> InvadersDestroyed => _invadersDestroyed;
    public Observable<Unit> GameRestarted { get; private set; }
    public Observable<Unit> GameStateChanged { get; private set; }

    public void Initialize()
    {
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
