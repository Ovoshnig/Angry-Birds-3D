using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.DataStorage.Storage;
using Ovoshnig.GameSettings;
using R3;
using System;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace AngryBirds3D.LevelState.Achievement;

public class LevelAchiever(LevelStateTracker levelTracker, SaveStorage saveStorage,
    SceneSettings sceneSettings) : IStartable, IDisposable
{
    private readonly LevelStateTracker _levelTracker = levelTracker;
    private readonly SaveStorage _saveStorage = saveStorage;
    private readonly SceneSettings _sceneSettings = sceneSettings;
    private readonly CompositeDisposable _disposables = [];

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

        if (currentLevel < _sceneSettings.ComingSoonSceneIndex
            && nextLevel > achievedLevel
            && nextLevel < sceneCount)
        {
            _saveStorage.Set(SaveConstants.AchievedLevelKey, nextLevel);
        }
    }
}
