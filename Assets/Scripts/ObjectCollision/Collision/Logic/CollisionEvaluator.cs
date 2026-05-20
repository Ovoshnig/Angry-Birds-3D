using UnityEngine;

public class CollisionEvaluator
{
    private readonly CollisionSettings _settings;

    public CollisionEvaluator(CollisionSettings settings) => _settings = settings;

    public bool TryEvaluate(CollisionData data, out CollisionType type, out float impactForce)
    {
        type = CollisionType.Collision;
        impactForce = 0f;

        if (data.ContactCount == 0)
            return false;

        impactForce = data.ImpulseMagnitude / Time.fixedDeltaTime;

        Vector3 contactNormal = data.ContactNormal;
        Vector3 impactVelocity = data.RelativeVelocity.normalized;

        float hitAngle = Mathf.Abs(Vector3.Dot(contactNormal, impactVelocity));
        bool isGliding = hitAngle < _settings.GlidingThreshold;

        if (impactForce >= _settings.DamageThreshold)
        {
            type = CollisionType.Damage;
            return true;
        }

        if (impactForce >= _settings.CollisionThreshold)
        {
            type = CollisionType.Collision;
            return true;
        }

        if (isGliding)
        {
            type = CollisionType.Gliding;
            return true;
        }

        return false;
    }
}
