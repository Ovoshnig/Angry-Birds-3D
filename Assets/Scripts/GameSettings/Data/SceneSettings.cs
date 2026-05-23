using System;
using UnityEngine;

[Serializable]
public class SceneSettings
{
    [field: SerializeField, Min(0)] public int FirstLevelIndex { get; private set; } = 1;
    [field: SerializeField, Min(1)] public int LevelCount { get; private set; } = 2;
    [field: SerializeField, Min(0.1f)] public float LevelTransitionDuration { get; private set; } = 5f;

    public int LastLevelIndex => FirstLevelIndex + LevelCount - 1;
    public int CreditsSceneIndex => LastLevelIndex + 1;
}
