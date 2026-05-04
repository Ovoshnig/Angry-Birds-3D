using Cysharp.Threading.Tasks;
using R3;
using System.Collections.Generic;

public class SceneSwitchMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly IReadOnlyList<SceneButtonView> _sceneButtonViews;

    public SceneSwitchMediator(SceneSwitch sceneSwitch,
        IReadOnlyList<SceneButtonView> sceneButtonViews)
    {
        _sceneSwitch = sceneSwitch;
        _sceneButtonViews = sceneButtonViews;
    }

    public override void Start()
    {
        foreach (var view in _sceneButtonViews)
        {
            view.Clicked
                .Subscribe(_ => _sceneSwitch.LoadSceneAsync(view.NavigationType, view.SpecificIndex).Forget())
                .AddTo(Disposables);
        }
    }
}
