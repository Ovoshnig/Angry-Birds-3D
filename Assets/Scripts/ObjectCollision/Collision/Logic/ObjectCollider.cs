using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public enum CollisionType
{
    Gliding, Collision, Damage
}

public record CollisionEvent<TView>(TView View, CollisionType Type, float Force)
    where TView : MonoBehaviour;

public abstract class ObjectCollider<TView> : IStartable, IDisposable
    where TView : MonoBehaviour
{
    private readonly TView[] _entityViews;
    private readonly CollisionSettings _collisionSettings;
    private readonly Subject<CollisionEvent<TView>> _collided = new();

    public ObjectCollider(TView[] entityViews, CollisionSettings collisionSettings)
    {
        _entityViews = entityViews;
        _collisionSettings = collisionSettings;
    }

    public Observable<CollisionEvent<TView>> Collided => _collided;

    public void Start()
    {
        foreach (var entityView in _entityViews)
        {
            ObjectColliderView colliderView = GetObjectColliderView(entityView);

            colliderView.Collided
                .Subscribe(collision => OnCollided(entityView, collision))
                .RegisterTo(colliderView.destroyCancellationToken);
        }
    }

    public void Dispose() => _collided.Dispose();

    protected abstract ObjectColliderView GetObjectColliderView(TView entityView);

    private void OnCollided(TView entityView, Collision collision)
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

        _collided.OnNext(new CollisionEvent<TView>(entityView, collisionType, impactForce));
    }
}
