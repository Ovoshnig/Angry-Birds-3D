using System;
using UnityEngine;

[Serializable]
public class BlockSettings
{
    [field: SerializeField, Min(0f)] public float WoodDamageMultiplier { get; private set; } = 0.002f;
    [field: SerializeField, Min(0f)] public float StoneDamageMultiplier { get; private set; } = 0.0012f;
    [field: SerializeField, Min(0f)] public float GlassDamageMultiplier { get; private set; } = 0.006f;
}
