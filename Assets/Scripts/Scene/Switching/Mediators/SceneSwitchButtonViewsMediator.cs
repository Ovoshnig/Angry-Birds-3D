using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.Scene.Switching;

public class SceneSwitchButtonViewsMediator(SceneSwitch sceneSwitch,
    IReadOnlyList<SceneSwitchButtonView> views) : UIViewsMediator<SceneSwitchButtonView>(views)
{
    private readonly SceneSwitch _sceneSwitch = sceneSwitch;

    protected override void OnViewEnabled(SceneSwitchButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _sceneSwitch.LoadSceneAsync(view.NavigationType, view.SpecificIndex).Forget())
            .AddTo(viewDisposables);
    }
}
