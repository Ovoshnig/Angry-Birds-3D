using Ovoshnig.DataStorage.Storage;
using Ovoshnig.GameSettings;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Ovoshnig.Scene.Switching;

public class SaveStorageSceneButtonViewsMediator(SaveStorage saveStorage,
    IReadOnlyList<SceneSwitchButtonView> views, SceneSettings sceneSettings)
    : UIViewsMediator<SceneSwitchButtonView>(views)
{
    private readonly SaveStorage _saveStorage = saveStorage;
    private readonly SceneSettings _sceneSettings = sceneSettings;

    private int _currentScene;

    public override void Start()
    {
        base.Start();

        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    protected override void OnViewEnabled(SceneSwitchButtonView view, CompositeDisposable viewDisposables)
    {
        int achievedLevel = _saveStorage.Get(SaveConstants.AchievedLevelKey, _sceneSettings.FirstLevelIndex);

        bool isInteractable = view.NavigationType switch
        {
            SceneNavigationType.SpecificIndex => view.SpecificIndex <= achievedLevel,
            SceneNavigationType.PreviousLevel => _currentScene > _sceneSettings.FirstLevelIndex,
            SceneNavigationType.NextLevel => _currentScene < achievedLevel,
            _ => true
        };

        view.SetInteractable(isInteractable);
    }
}
