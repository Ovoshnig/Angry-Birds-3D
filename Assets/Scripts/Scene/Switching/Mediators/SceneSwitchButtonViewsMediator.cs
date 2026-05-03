using Cysharp.Threading.Tasks;
using R3;

public class SceneSwitchButtonViewsMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly SceneButtonView[] _sceneButtonViews;

    public SceneSwitchButtonViewsMediator(SceneSwitch sceneSwitch,
        SceneButtonView[] sceneButtonViews)
    {
        _sceneSwitch = sceneSwitch;
        _sceneButtonViews = sceneButtonViews;
    }

    public override void Start()
    {
        foreach (var sceneView in _sceneButtonViews)

        sceneView.Clicked
            .Subscribe(_ => OnButtonClicked(sceneView))
            .AddTo(Disposables);
    }

    private void OnButtonClicked(SceneButtonView sceneButtonView)
    {
        switch (sceneButtonView)
        {
            case MainMenuButtonView:
                _sceneSwitch.LoadLevelAsync(0).Forget();
                break;
            case CurrentLevelButtonView:
                _sceneSwitch.LoadCurrentLevelAsync().Forget();
                break;
            case NextLevelButtonView:
                _sceneSwitch.LoadNextLevelAsync().Forget();
                break;
            default:
                throw new System.Exception($"Unknown scene button view type: " +
                    $"{_sceneButtonViews.GetType().Name}");
        }
    }
}
