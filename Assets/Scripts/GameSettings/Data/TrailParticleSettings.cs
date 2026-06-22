using System;
using UnityEngine;

[Serializable]
public class TrailParticleSettings
{
    [field: SerializeField] public int PoolDefaultCapacity { get; private set; } = 3;
    [field: SerializeField] public int PoolMaxSize { get; private set; } = 6;
}
