using System;
using UnityEngine;

namespace Ovoshnig.GameSettings
{
    [Serializable]
    public class LevelStateTrackingSettings
    {
        [field: SerializeField, Min(0f)] public float ActivityTimeout { get; private set; } = 2.5f;
    }
}
