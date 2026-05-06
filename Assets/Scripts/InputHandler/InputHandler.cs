using R3;
using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

public abstract class InputHandler<TActions> : IStartable, IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    protected InputHandler(TActions actions) => Actions = actions;

    protected TActions Actions { get; }

    public virtual void Start() => EnableActions();

    public virtual void Dispose()
    {
        _disposables.Dispose();

        DisableActions();
    }

    protected abstract void EnableActions();

    protected abstract void DisableActions();

    protected ReadOnlyReactiveProperty<T> BindValue<T>(Func<TActions, InputAction> selector) where T : struct =>
        selector(Actions).ToValueProperty<T>().AddTo(_disposables);

    protected ReadOnlyReactiveProperty<bool> BindButton(Func<TActions, InputAction> selector)
        => selector(Actions).ToButtonProperty().AddTo(_disposables);
}
