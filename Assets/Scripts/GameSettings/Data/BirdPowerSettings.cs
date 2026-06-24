using System;
using UnityEngine;

[Serializable]
public class BirdPowerSettings
{
    [field: SerializeField, Min(0f)] public float SplitAngleDiff { get; private set; } = 15f;
}
