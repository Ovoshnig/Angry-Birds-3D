using R3;
using System.Collections.Generic;

public class SaveStorageSceneButtonViewsMediator : UIListMediator<SceneButtonView>
{
    private readonly SaveStorage _saveStorage;
    private readonly SceneSettings _sceneSettings;

    public SaveStorageSceneButtonViewsMediator(SaveStorage saveStorage,
        IReadOnlyList<SceneButtonView> views,
        SceneSettings sceneSettings) : base(views)
    {
        _saveStorage = saveStorage;
        _sceneSettings = sceneSettings;
    }

    protected override void OnViewEnabled(SceneButtonView view, CompositeDisposable viewDisposables)
    {
        int achievedLevel = _saveStorage.Get(SaveConstants.AchievedLevelKey, _sceneSettings.FirstLevelIndex);
        view.SetInteractable(view.SpecificIndex <= achievedLevel);
    }
}
