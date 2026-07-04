using System;
using UnityEngine;

[Serializable]
public class EggDroppingPowerSettings
{
    [field: SerializeField, Min(0f)] public float RecoilForce { get; private set; } = 8f;
    [field: SerializeField, Min(0f)] public float ExplosionForce { get; private set; } = 500f;
    [field: SerializeField, Min(0f)] public float ExplosionRadius { get; private set; } = 4f;
    [field: SerializeField, Min(0f)] public float UpwardsModifier { get; private set; } = 1f;
    [field: SerializeField, Min(0)] public int MaxExplosiveCount { get; private set; } = 30;
}
