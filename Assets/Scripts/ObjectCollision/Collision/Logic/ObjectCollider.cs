using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class ObjectCollider : IStartable, IDisposable
{
    private readonly IReadOnlyList<CollidableEntityView> _entityViews;
    private readonly CollisionSettings _collisionSettings;
    private readonly Subject<CollisionEvent> _collided = new();

    public ObjectCollider(IReadOnlyList<CollidableEntityView> entityViews,
        CollisionSettings collisionSettings)
    {
        _entityViews = entityViews;
        _collisionSettings = collisionSettings;
    }

    public Observable<CollisionEvent> Collided => _collided;

    public void Start()
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

        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        Vector3 contactNormal = collision.GetContact(0).normal;
        Vector3 impactVelocity = collision.relativeVelocity.normalized;

        float hitAngle = Mathf.Abs(Vector3.Dot(contactNormal, impactVelocity));
        bool isGliding = hitAngle < _collisionSettings.GlidingThreshold;

        CollisionType collisionType;

        if (impactForce >= _collisionSettings.DamageThreshold)
            collisionType = CollisionType.Damage;
        else if (impactForce >= _collisionSettings.CollisionThreshold)
            collisionType = CollisionType.Collision;
        else if (isGliding)
            collisionType = CollisionType.Gliding;
        else
            return;

        _collided.OnNext(new CollisionEvent(entityView, collisionType, impactForce));
    }
}
