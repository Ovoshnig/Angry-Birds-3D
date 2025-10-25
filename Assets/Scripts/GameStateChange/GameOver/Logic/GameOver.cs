using R3;
using System;
using VContainer.Unity;

public class GameOver : IInitializable, IDisposable
{
    private readonly GameRestarter _gameRestarter;
    private readonly ReactiveProperty<bool> _isOver = new(false);
    private readonly CompositeDisposable _compositeDisposable = new();

    public GameOver(GameRestarter gameRestarter) => _gameRestarter = gameRestarter;

    public ReadOnlyReactiveProperty<bool> IsOver => _isOver;

    public void Initialize()
    {
        _gameRestarter.Restarted
            .Subscribe(_ => _isOver.Value = false)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
