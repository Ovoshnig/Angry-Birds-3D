using System;
using UnityEngine;

[Serializable]
public class ExplosionPowerSettings
{
    [field: SerializeField, Min(0f)] public float ExplosionDelay { get; private set; } = 1.5f;
    [field: SerializeField, Min(0f)] public float ExplosionForce { get; private set; } = 700f;
    [field: SerializeField, Min(0f)] public float ExplosionRadius { get; private set; } = 6f;
    [field: SerializeField, Min(0f)] public float UpwardsModifier { get; private set; } = 2f;
    [field: SerializeField, Min(0)] public int MaxExplosiveCount { get; private set; } = 30;
}
