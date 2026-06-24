using UnityEngine;

public class CollisionEvaluator
{
    private readonly CollisionSettings _settings;

    public CollisionEvaluator(CollisionSettings settings) => _settings = settings;

    public bool TryEvaluate(CollisionRawData rawData, out CollisionType type, out float impactForce)
    {
        type = CollisionType.Collision;
        impactForce = 0f;

        if (rawData.ContactCount == 0)
            return false;

        impactForce = rawData.ImpulseMagnitude;

        Vector3 contactNormal = rawData.ContactNormal;
        Vector3 impactVelocity = rawData.RelativeVelocity.normalized;

        float hitAngle = Mathf.Abs(Vector3.Dot(contactNormal, impactVelocity));
        bool isGliding = hitAngle < _settings.GlidingThreshold;

        if (impactForce >= _settings.DamageThreshold)
        {
            type = CollisionType.Damage;
            return true;
        }

        if (impactForce >= _settings.CollisionThreshold)
        {
            if (!isGliding)
            {
                type = CollisionType.Collision;
                return true;
            }

            type = CollisionType.Gliding;
            return true;
        }

        return false;
    }
}
