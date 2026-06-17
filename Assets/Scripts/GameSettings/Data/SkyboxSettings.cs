using System;
using UnityEngine;

[Serializable]
public class SkyboxSettings
{
    [field: SerializeField] public float LoopDuration { get; private set; } = 360f;
}
