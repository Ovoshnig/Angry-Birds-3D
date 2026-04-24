using R3;
using UnityEngine;

public enum CollisionType
{
    Gliding, Collision, Damage
}

public record CollisionEvent<TView>(TView View, CollisionType Type, float Force)
    where TView : MonoBehaviour;

public class ObjectCollider<TView> where TView : MonoBehaviour
{
    private readonly CollisionSettings _collisionSettings;
    private readonly Subject<CollisionEvent<TView>> _collided = new();

    public ObjectCollider(CollisionSettings collisionSettings) => 
        _collisionSettings = collisionSettings;

    public Observable<CollisionEvent<TView>> Collided => _collided;

    public void Collide(TView view, Collision collision)
    {
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        Vector3 contactNormal = collision.contacts[0].normal;
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

        _collided.OnNext(new CollisionEvent<TView>(view, collisionType, impactForce));
    }
}
