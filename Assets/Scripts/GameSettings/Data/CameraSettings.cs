using System;
using UnityEngine;

[Serializable]
public class CameraSettings
{
    [field: SerializeField, Min(0f)] public float StructureShowingDuration { get; private set; } = 2f;
    [field: SerializeField, Min(0f)] public float SlingshotShowingDuration { get; private set; } = 1.5f;
}
