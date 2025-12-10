using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    [field: SerializeField] public int BlockPoints { get; private set; } = 500;
    [field: SerializeField] public int PigPoints { get; private set; } = 5000;
    [field: SerializeField] public int PoolDefaultCapacity { get; private set; } = 5;
    [field: SerializeField] public int PoolMaxSize { get; private set; } = 10;
}
