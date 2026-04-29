using R3;
using System;
using VContainer.Unity;

public abstract class Mediator : IStartable, IDisposable
{
    protected CompositeDisposable CompositeDisposable { get; } = new();

    public abstract void Start();

    public virtual void Dispose() => CompositeDisposable.Dispose();
}
