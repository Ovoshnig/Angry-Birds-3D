using Ovoshnig.DataStorage.Storage;
using Ovoshnig.GameSettings;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Ovoshnig.Scene.Switching
{
    public class SaveStorageSceneSwitchPanelsMediator : UIViewsMediator<SceneSwitchPanelView>
    {
        private readonly SaveStorage _saveStorage;
        private readonly SceneSettings _sceneSettings;

        private int _firstLevel;
        private int _currentScene;

        public SaveStorageSceneSwitchPanelsMediator(SaveStorage saveStorage,
            IReadOnlyList<SceneSwitchPanelView> views,
            SceneSettings sceneSettings) : base(views)
        {
            _saveStorage = saveStorage;
            _sceneSettings = sceneSettings;
        }

        public override void Start()
        {
            base.Start();

            _firstLevel = _sceneSettings.FirstLevelIndex;
            _currentScene = SceneManager.GetActiveScene().buildIndex;
        }

        protected override void OnViewEnabled(SceneSwitchPanelView view, CompositeDisposable viewDisposables)
        {
            int achievedLevel = _saveStorage.Get(SaveConstants.AchievedLevelKey, _sceneSettings.FirstLevelIndex);
            view.UpdateButtonEnabled(_firstLevel, _currentScene, achievedLevel);
        }
    }
}
