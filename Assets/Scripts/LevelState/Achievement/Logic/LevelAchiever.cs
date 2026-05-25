using R3;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class LevelAchiever : IStartable, IDisposable
{
    private readonly LevelStateTracker _levelTracker;
    private readonly SaveStorage _saveStorage;
    private readonly SceneSettings _sceneSettings;
    private readonly CompositeDisposable _disposables = new();

    public LevelAchiever(LevelStateTracker levelTracker,
        SaveStorage saveStorage,
        SceneSettings sceneSettings)
    {
        _levelTracker = levelTracker;
        _saveStorage = saveStorage;
        _sceneSettings = sceneSettings;
    }

    public void Start()
    {
        _levelTracker.Cleared
            .Subscribe(_ => OnLevelCleared())
            .AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();

    private void OnLevelCleared()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        int achievedLevel = _saveStorage.Get(SaveConstants.AchievedLevelKey, _sceneSettings.FirstLevelIndex);

        if (currentLevel < _sceneSettings.LastLevelIndex
            && nextLevel > achievedLevel
            && nextLevel < sceneCount)
            _saveStorage.Set(SaveConstants.AchievedLevelKey, nextLevel);
    }
}
