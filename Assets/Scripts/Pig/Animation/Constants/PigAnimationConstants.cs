using UnityEngine;

namespace AngryBirds3D.Pig.Animation
{
    public static class PigAnimationConstants
    {
        public const string HealthParameterName = "Health";

        public static readonly int HealthParameterId = Animator.StringToHash(HealthParameterName);
    }
}
