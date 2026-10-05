using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.Scene.Switching
{
    public class SceneSwitchButtonViewsMediator : UIViewsMediator<SceneSwitchButtonView>
    {
        private readonly SceneSwitch _sceneSwitch;

        public SceneSwitchButtonViewsMediator(SceneSwitch sceneSwitch, IReadOnlyList<SceneSwitchButtonView> views)
            : base(views) => _sceneSwitch = sceneSwitch;

        protected override void OnViewEnabled(SceneSwitchButtonView view, CompositeDisposable viewDisposables)
        {
            view.Clicked
                .Subscribe(_ => _sceneSwitch.LoadSceneAsync(view.NavigationType, view.SpecificIndex).Forget())
                .AddTo(viewDisposables);
        }
    }
}
