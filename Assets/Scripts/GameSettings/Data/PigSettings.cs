using System;
using UnityEngine;

[Serializable]
public class PigSettings
{
    [field: SerializeField, Min(0f)] public float Health { get; private set; } = 100f;
}
