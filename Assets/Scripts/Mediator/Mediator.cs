using R3;
using System;
using VContainer.Unity;

public abstract class Mediator : IStartable, IDisposable
{
    protected CompositeDisposable Disposables { get; } = new();

    public abstract void Start();

    public virtual void Dispose() => Disposables.Dispose();
}
