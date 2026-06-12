using LitMotion;
using System;
using UnityEngine;

[Serializable]
public class BirdStretchSettings
{
    [field: SerializeField, Min(0f)] public float StretchDelay { get; private set; } = 0.2f;
    [field: SerializeField, Min(0f)] public float StretchDuration { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float MinVelocitySquareMagnitude { get; private set; } = 0f;
    [field: SerializeField, Min(0f)] public float MaxVelocitySquareMagnitude { get; private set; } = 800f;
    [field: SerializeField] public Vector3 MaxStretchScale { get; private set; } = new(0.6f, 0.6f, 2f);
    [field: SerializeField] public Ease StretchEase { get; private set; } = Ease.OutQuad;
}
