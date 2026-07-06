using System;
using UnityEngine;

[Serializable]
public class SceneSettings
{
    [field: SerializeField, Min(0)] public int FirstLevelIndex { get; private set; } = 1;
    [field: SerializeField, Min(1)] public int LevelCount { get; private set; } = 2;
    [field: SerializeField, Min(0f)] public float InterpolationSpeed { get; private set; } = 10f;

    public int MainMenuIndex => FirstLevelIndex - 1;
    public int LastLevelIndex => FirstLevelIndex + LevelCount - 1;
    public int ComingSoonSceneIndex => LastLevelIndex + 1;
}
