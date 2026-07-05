using System;
using UnityEngine;

[Serializable]
public class LevelStateTrackingSettings
{
    [field: SerializeField, Min(0f)] public float ActivityTimeout { get; private set; } = 2.5f;
}
