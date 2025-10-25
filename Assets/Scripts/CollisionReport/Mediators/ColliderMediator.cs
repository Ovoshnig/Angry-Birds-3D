using R3;
using System;
using UnityEngine;

public abstract class CollisionMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly CollisionReporter<TView> _collisionReporter;

    protected CollisionMediator(CollisionReporter<TView> collisionReporter) =>
        _collisionReporter = collisionReporter;

    protected void Subscribe(TView entityView, TriggerColliderView colliderView)
    {
        colliderView.Collided
            .Subscribe(other => _collisionReporter.Report(entityView, other))
            .AddTo(CompositeDisposable);
    }

    protected void Subscribe(TView entityView,
        TriggerColliderView colliderView,
        out IDisposable disposable)
    {
        disposable = colliderView.Collided
            .Subscribe(other => _collisionReporter.Report(entityView, other));
    }
}
