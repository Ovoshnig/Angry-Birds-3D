using System;
using UnityEngine;

[Serializable]
public class BlockSettings
{
    [field: SerializeField, Min(0f)] public float WoodDestructionMultiplier { get; private set; } = 0.02f;
    [field: SerializeField, Min(0f)] public float StoneDestructionMultiplier { get; private set; } = 0.03f;
    [field: SerializeField, Min(0f)] public float GlassDestructionMultiplier { get; private set; } = 0.01f;
}
