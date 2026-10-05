using System;
using UnityEngine;

namespace Ovoshnig.GameSettings
{
    [Serializable]
    public class BirdSettings
    {
        [field: SerializeField, Min(0f)] public float DestructionDelay { get; private set; } = 4f;
        [field: SerializeField, Min(0f)] public float FallOutDepth { get; private set; } = 10f;
    }
}
