using System;
using UnityEngine;

[Serializable]
public class BirdSettings
{
    [field: SerializeField] public float RotationSpeedThreshold { get; private set; } = 0.5f;
}
