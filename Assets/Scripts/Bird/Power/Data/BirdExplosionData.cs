using UnityEngine;
using UnityEngine.Audio;

namespace AngryBirds3D.Bird.Power
{
    public record BirdExplosionData(Transform Transform, float Force, float Radius, AudioResource AudioResource);
}
