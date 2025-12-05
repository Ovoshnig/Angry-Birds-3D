using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    [field: SerializeField] public int BlockPoints { get; private set; } = 500;
}
