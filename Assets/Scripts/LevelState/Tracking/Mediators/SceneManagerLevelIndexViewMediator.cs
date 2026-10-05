using Ovoshnig.GameSettings;
using Ovoshnig.UI.Basic;
using R3;
using UnityEngine.SceneManagement;

namespace AngryBirds3D.LevelState.Tracking
{
    public class SceneManagerLevelIndexViewMediator : UIViewMediator<LevelIndexView>
    {
        private readonly SceneSettings _sceneSettings;

        public SceneManagerLevelIndexViewMediator(SceneSettings sceneSettings, LevelIndexView view)
            : base(view) => _sceneSettings = sceneSettings;

        protected override void OnViewEnabled(LevelIndexView view, CompositeDisposable viewDisposables)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int currentLevelIndex = currentSceneIndex - _sceneSettings.FirstLevelIndex + 1;
            view.SetIndex(1, currentLevelIndex);
        }
    }
}
