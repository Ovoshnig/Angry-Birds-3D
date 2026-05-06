using R3;
using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

public abstract class InputProvider<TActions> : IStartable, IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    protected InputProvider(TActions actions) => Actions = actions;

    protected TActions Actions { get; }

    public virtual void Start() => EnableActions();

    public virtual void Dispose()
    {
        _disposables.Dispose();
        DisableActions();
    }

    protected abstract void EnableActions();

    protected abstract void DisableActions();

    protected ReadOnlyReactiveProperty<bool> ObserveButton(Func<TActions, InputAction> selector)
        => selector(Actions).ToButtonProperty().AddTo(_disposables);

    protected ReadOnlyReactiveProperty<T> ObserveValue<T>(Func<TActions, InputAction> selector) where T : struct =>
        selector(Actions).ToValueProperty<T>().AddTo(_disposables);
}
