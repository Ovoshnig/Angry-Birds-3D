using R3;
using System;
using VContainer.Unity;

namespace Ovoshnig.Mediation;

public abstract class Mediator : IStartable, IDisposable
{
    private readonly CompositeDisposable _disposables = [];

    public virtual void Start() => Bind(_disposables);

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        Unbind();
    }

    protected abstract void Bind(CompositeDisposable disposables);

    protected virtual void Unbind() => _disposables.Dispose();
}
