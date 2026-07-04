using UnityEngine;
using UnityEngine.Audio;

public record BirdExplosionData(Transform Transform, float Force, float Radius, AudioResource AudioResource);
