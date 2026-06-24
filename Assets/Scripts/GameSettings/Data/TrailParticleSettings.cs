using System;
using UnityEngine;

[Serializable]
public class TrailParticleSettings
{
    [field: SerializeField] public int PoolDefaultCapacity { get; private set; } = 3;
    [field: SerializeField] public int PoolMaxSize { get; private set; } = 6;
    [field: SerializeField, Min(0f)] public float PowerParticleSize { get; private set; } = 0.75f;
}
