using System;
using UnityEngine;

[Serializable]
public class CollisionSettings
{
    public CollisionSettings()
    {
    }

    public CollisionSettings(float glidingThreshold, float collisionThreshold, float damageThreshold)
    {
        GlidingThreshold = glidingThreshold;
        CollisionThreshold = collisionThreshold;
        DamageThreshold = damageThreshold;
    }

    [field: SerializeField, Min(0f)] public float GlidingThreshold { get; private set; } = 0.4f;
    [field: SerializeField, Min(0f)] public float CollisionThreshold { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float DamageThreshold { get; private set; } = 20f;
}
