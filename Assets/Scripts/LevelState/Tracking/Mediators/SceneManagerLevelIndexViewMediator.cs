using Ovoshnig.GameSettings;
using Ovoshnig.UI.Basic;
using R3;
using UnityEngine.SceneManagement;

namespace AngryBirds3D.LevelState.Tracking;

public class SceneManagerLevelIndexViewMediator(SceneSettings sceneSettings, LevelIndexView view)
    : UIViewMediator<LevelIndexView>(view)
{
    private readonly SceneSettings _sceneSettings = sceneSettings;

    protected override void OnViewEnabled(LevelIndexView view, CompositeDisposable viewDisposables)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int currentLevelIndex = currentSceneIndex - _sceneSettings.FirstLevelIndex + 1;
        view.SetIndex(1, currentLevelIndex);
    }
}
