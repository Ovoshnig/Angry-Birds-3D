using System.Collections.Generic;
using UnityEngine;

public sealed class SceneMusicMapper : ISceneMusicMapper
{
    private readonly Dictionary<SceneType, MusicCategory> _sceneToMusicCategory = new()
    {
        { SceneType.MainMenu, MusicCategory.MainMenu },
        { SceneType.GameLevel, MusicCategory.GameLevel },
        { SceneType.Credits, MusicCategory.Credits }
    };

    public MusicCategory GetMusicCategory(SceneType sceneType)
    {
        if (_sceneToMusicCategory.TryGetValue(sceneType, out var category))
            return category;

        Debug.LogWarning($"No suitable music category for {sceneType} scene type");
        return MusicCategory.MainMenu;
    }
}
