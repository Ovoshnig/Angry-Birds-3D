using System;
using UnityEngine;

[Serializable]
public class BirdSettings
{
    [field: SerializeField, Min(0f)] public float DestructionDelay { get; private set; } = 4f;
    [field: SerializeField, Min(0f)] public float PointsDisplayDelay { get; private set; } = 2f;
}
