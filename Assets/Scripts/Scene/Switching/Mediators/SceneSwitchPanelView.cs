using Cysharp.Threading.Tasks;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;

namespace Ovoshnig.Scene.Switching;

public class SceneSwitchPanelsMediator(SceneSwitch sceneSwitch, IReadOnlyList<SceneSwitchPanelView> views)
    : UIViewsMediator<SceneSwitchPanelView>(views)
{
    private readonly SceneSwitch _sceneSwitch = sceneSwitch;

    protected override void OnViewEnabled(SceneSwitchPanelView view, CompositeDisposable viewDisposables)
    {
        view.NavigationRequested
            .Subscribe(request => _sceneSwitch.LoadSceneAsync(request.Type, request.Index).Forget())
            .AddTo(viewDisposables);
    }
}
