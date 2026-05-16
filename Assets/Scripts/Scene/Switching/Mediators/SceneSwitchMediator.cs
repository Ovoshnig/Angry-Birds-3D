using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;

public class SceneSwitchMediator : UIListMediator<SceneButtonView>
{
    private readonly SceneSwitch _sceneSwitch;

    public SceneSwitchMediator(SceneSwitch sceneSwitch, IReadOnlyList<SceneButtonView> views)
        : base(views) => _sceneSwitch = sceneSwitch;

    protected override void OnViewEnabled(SceneButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _sceneSwitch.LoadSceneAsync(view.NavigationType, view.SpecificIndex).Forget())
            .AddTo(viewDisposables);
    }
}
