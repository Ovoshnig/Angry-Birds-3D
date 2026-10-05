using System;
using UnityEngine;

namespace Ovoshnig.GameSettings
{
    [Serializable]
    public class SkyboxSettings
    {
        [field: SerializeField] public float LoopDuration { get; private set; } = 360f;
    }
}
