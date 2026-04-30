using R3;
using System;
using UnityEngine.InputSystem;
using VContainer.Unity;

public abstract class InputHandler<TActions> : IStartable, IDisposable
{
    private readonly CompositeDisposable _compositeDisposable = new();

    protected InputHandler(TActions actions) => Actions = actions;

    protected TActions Actions { get; }

    public virtual void Start() => EnableActions();

    public virtual void Dispose()
    {
        _compositeDisposable.Dispose();
        DisableActions();
    }

    protected abstract void EnableActions();

    protected abstract void DisableActions();

    protected ReadOnlyReactiveProperty<T> BindValue<T>(Func<TActions, InputAction> selector) where T : struct =>
        selector(Actions).AsValueStream<T>().AddTo(_compositeDisposable);

    protected ReadOnlyReactiveProperty<bool> BindButton(Func<TActions, InputAction> selector)
        => selector(Actions).AsButtonStream().AddTo(_compositeDisposable);
}
