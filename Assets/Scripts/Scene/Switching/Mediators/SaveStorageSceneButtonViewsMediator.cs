using R3;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveStorageSceneButtonViewsMediator : UIListMediator<SceneButtonView>
{
    private readonly SaveStorage _saveStorage;
    private readonly SceneSettings _sceneSettings;

    private int _currentScene;

    public SaveStorageSceneButtonViewsMediator(SaveStorage saveStorage,
        IReadOnlyList<SceneButtonView> views,
        SceneSettings sceneSettings) : base(views)
    {
        _saveStorage = saveStorage;
        _sceneSettings = sceneSettings;
    }

    public override void Start()
    {
        base.Start();

        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    protected override void OnViewEnabled(SceneButtonView view, CompositeDisposable viewDisposables)
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
