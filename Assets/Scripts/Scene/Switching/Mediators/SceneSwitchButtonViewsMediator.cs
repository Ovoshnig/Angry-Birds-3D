using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;

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
