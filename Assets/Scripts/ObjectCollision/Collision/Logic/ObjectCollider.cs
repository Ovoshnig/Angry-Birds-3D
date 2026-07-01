using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollider : IDisposable
{
    private readonly IReadOnlyList<CollidableEntityView> _entityViews;
    private readonly CollisionEvaluator _evaluator;
    private readonly Subject<CollisionData> _collided = new();

    public ObjectCollider(IReadOnlyList<CollidableEntityView> entityViews,
        CollisionEvaluator evaluator)
    {
        _entityViews = entityViews;
        _evaluator = evaluator;
    }

    public Observable<CollisionData> Collided => _collided;

    public void Subscribe()
    {
        foreach (var entityView in _entityViews)
        {
            ObjectColliderView colliderView = entityView.ColliderView;

            colliderView.Collided
                .Subscribe(collision => OnCollided(entityView, collision))
                .RegisterTo(colliderView.destroyCancellationToken);
        }
    }

    public void Dispose() => _collided.Dispose();

    private void OnCollided(CollidableEntityView entityView, Collision collision)
    {
        if (collision.contactCount == 0)
            return;

        CollisionRawData rawData = new(collision.GetContact(0).normal,
            collision.relativeVelocity,
            collision.impulse.magnitude,
            collision.contactCount);

        if (_evaluator.TryEvaluate(rawData, out CollisionType type, out float force))
            _collided.OnNext(new CollisionData(entityView, type, collision.GetContact(0).point, force));
    }
}
