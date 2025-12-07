using System;
using UnityEngine;

[Serializable]
public class PigSettings
{
    [field: SerializeField, Min(0f)] public float DamageThreshold { get; private set; } = 0.2f;
    [field: SerializeField, Min(0f)] public float Health { get; private set; } = 100f;
}
