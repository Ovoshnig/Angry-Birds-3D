using R3;
using System;
using VContainer.Unity;

public abstract class Mediator : IStartable, IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    public virtual void Start() => Bind(_disposables);

    public virtual void Dispose() => Unbind();

    protected abstract void Bind(CompositeDisposable disposables);

    protected virtual void Unbind() => _disposables.Dispose();
}
