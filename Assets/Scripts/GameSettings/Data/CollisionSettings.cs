using System;
using UnityEngine;

[Serializable]
public class CollisionSettings
{
    [field: SerializeField, Min(0f)] public float GlidingThreshold { get; private set; } = 0.2f;
    [field: SerializeField, Min(0f)] public float DamageThreshold { get; private set; } = 5f;
    [field: SerializeField, Min(0f)] public float CollisionThreshold { get; private set; } = 0.001f;
}
