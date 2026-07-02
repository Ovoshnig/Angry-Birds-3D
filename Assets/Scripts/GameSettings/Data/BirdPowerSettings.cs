using LitMotion;
using System;
using UnityEngine;

[Serializable]
public class BirdPowerSettings
{
    [field: SerializeField, Min(0f)] public float SplitAngleDiff { get; private set; } = 15f;

    [field: SerializeField, Min(0f)] public float VelocityIncreasingDuration { get; private set; } = 0.1f;
    [field: SerializeField, Min(0f)] public float BoostDuration { get; private set; } = 0.12f;
    [field: SerializeField, Min(0f)] public float BoostVelocity { get; private set; } = 40f;
    [field: SerializeField] public Ease VelocityIncreasingEase { get; private set; } = Ease.OutSine;

    [field: SerializeField, Min(0f)] public float ExplosionDelay { get; private set; } = 1.5f;
    [field: SerializeField, Min(0f)] public float ExplosionForce { get; private set; } = 100f;
    [field: SerializeField, Min(0f)] public float ExplosionRadius { get; private set; } = 4f;
    [field: SerializeField, Min(0f)] public float UpwardsModifier { get; private set; } = 1f;
    [field: SerializeField, Min(0)] public int MaxExplosiveCount { get; private set; } = 30;
}
