using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    [field: SerializeField] public int PoolDefaultCapacity { get; private set; } = 5;
    [field: SerializeField] public int PoolMaxSize { get; private set; } = 10;
}
