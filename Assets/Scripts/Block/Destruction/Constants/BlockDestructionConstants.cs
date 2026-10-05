using UnityEngine;

namespace AngryBirds3D.Block.Destruction
{
    public static class BlockDestructionConstants
    {
        public static int[] HitProperties { get; } = new[]
        {
            Shader.PropertyToID("_Hit0"),
            Shader.PropertyToID("_Hit1"),
            Shader.PropertyToID("_Hit2"),
            Shader.PropertyToID("_Hit3")
        };
    }
}
