using LitMotion;
using System;
using UnityEngine;

[Serializable]
public class SlingshotPlacingSettings
{
    [field: Header("Movement Settings")]
    [field: SerializeField, Min(0f)] public float JumpHeight { get; private set; } = 4f;
    [field: SerializeField, Min(0f)] public float PlacingDuration { get; private set; } = 1f;

    [field: Header("Scale Settings")]
    [field: SerializeField, Min(0f)] public float SquashDuration { get; private set; } = 0.2f;
    [field: SerializeField] public Vector3 SquashScale { get; private set; } = new(1.25f, 0.7f, 1.25f);
    [field: SerializeField] public Vector3 StretchScale { get; private set; } = new(0.8f, 1.3f, 0.8f);

    [field: Header("Ease Settings")]
    [field: SerializeField] public Ease SquashEase { get; private set; } = Ease.OutQuad;
    [field: SerializeField] public Ease JumpEase { get; private set; } = Ease.InOutQuad;
    [field: SerializeField] public Ease RiseScaleEase { get; private set; } = Ease.OutQuad;
    [field: SerializeField] public Ease FallScaleEase { get; private set; } = Ease.InQuad;
}
