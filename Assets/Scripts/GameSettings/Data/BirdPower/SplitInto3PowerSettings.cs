using System;
using UnityEngine;

namespace Ovoshnig.GameSettings.BirdPower
{
    [Serializable]
    public class SplitInto3PowerSettings
    {
        [field: SerializeField, Min(0f)] public float SplitAngleDiff { get; private set; } = 15f;
    }
}
