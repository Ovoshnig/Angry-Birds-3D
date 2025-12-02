using System;
using UnityEngine;

[Serializable]
public class SlingshotSettings
{
    [field: SerializeField] public LayerMask SlingshotLayer { get; private set; }
    [field: SerializeField] public float LaunchForce { get; private set; } = 8f;
    [field: SerializeField] public float MaxDragDistance { get; private set; } = 3f;
    [field: SerializeField] public float InputInteractionRadius { get; private set; } = 2.5f;

    [field: SerializeField, Range(0, 0.5f)] public float SkinOffset { get; private set; } = 0.18f;
    [field: SerializeField, Range(1, 40)] public int SegmentCount { get; private set; } = 30;
    [field: SerializeField, Range(1f, 2f)] public float TangentScale { get; private set; } = 1.7f;
    [field: SerializeField, Range(0f, 1f)] public float RubberWrapOffset { get; private set; } = 0.3f;
    [field: SerializeField] public float SlingshotCollisionOffset { get; private set; } = 0.3f;
}
