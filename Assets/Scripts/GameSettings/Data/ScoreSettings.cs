using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    [field: SerializeField] public int BlockPoints { get; private set; } = 500;
    [field: SerializeField] public int PigPoints { get; private set; } = 5000;
}
