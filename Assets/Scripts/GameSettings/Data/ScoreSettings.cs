using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    [field: SerializeField, Min(0)] public int BlockPoints { get; private set; } = 500;
    [field: SerializeField, Min(0)] public int PigPoints { get; private set; } = 5000;
    [field: SerializeField] public Color BlockColor { get; private set; } = Color.white;
    [field: SerializeField] public Color PigColor { get; private set; } = Color.lightGreen;
    [field: SerializeField, Min(0f)] public float BlockFontSize { get; private set; } = 1f;
    [field: SerializeField, Min(0f)] public float PigFontSize { get; private set; } = 1.5f;

    [field: SerializeField] public int PoolDefaultCapacity { get; private set; } = 5;
    [field: SerializeField] public int PoolMaxSize { get; private set; } = 10;
}
