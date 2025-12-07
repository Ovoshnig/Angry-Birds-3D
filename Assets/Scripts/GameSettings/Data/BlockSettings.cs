using System;
using UnityEngine;

[Serializable]
public class BlockSettings
{
    [field: SerializeField, Min(0f)] public float DamageThreshold { get; private set; } = 0.2f;
    [field: SerializeField, Min(0f)] public float WoodHealth { get; private set; } = 600f;
    [field: SerializeField, Min(0f)] public float StoneHealth { get; private set; } = 1200f;
    [field: SerializeField, Min(0f)] public float GlassHealth { get; private set; } = 200f;
}
