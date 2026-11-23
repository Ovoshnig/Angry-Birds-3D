using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;

namespace Ovoshnig.Scene.Switching
{
    public class SceneSwitchPanelMediator : UIViewMediator<SceneSwitchPanelView>
    {
        private readonly SceneSwitch _sceneSwitch;

        public SceneSwitchPanelMediator(SceneSwitch sceneSwitch, SceneSwitchPanelView view) : base(view) =>
            _sceneSwitch = sceneSwitch;

        protected override void OnViewEnabled(SceneSwitchPanelView view, CompositeDisposable viewDisposables)
        {
            view.NavigationRequested
                .Subscribe(request => _sceneSwitch.LoadSceneAsync(request.Type, request.Index).Forget())
                .AddTo(viewDisposables);
        }
    }
}
