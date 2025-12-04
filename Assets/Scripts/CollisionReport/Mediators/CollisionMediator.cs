using R3;
using System;
using UnityEngine;

public abstract class CollisionMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly CollisionReporter<TView> _collisionReporter;

    protected CollisionMediator(CollisionReporter<TView> collisionReporter) =>
        _collisionReporter = collisionReporter;

    protected void Subscribe(TView view, CollisionView collisionView)
    {
        collisionView.Collided
            .Subscribe(collision => _collisionReporter.Report(view, collision))
            .AddTo(CompositeDisposable);
    }

    protected void Subscribe(TView view,
        CollisionView collisionView,
        out IDisposable disposable)
    {
        disposable = collisionView.Collided
            .Subscribe(collision => _collisionReporter.Report(view, collision));
    }
}
