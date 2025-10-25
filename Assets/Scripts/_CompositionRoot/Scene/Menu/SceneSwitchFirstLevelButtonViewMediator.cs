using Cysharp.Threading.Tasks;
using R3;

public class SceneSwitchFirstLevelButtonViewMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly FirstLevelButtonView _firstLevelButtonView;

    public SceneSwitchFirstLevelButtonViewMediator(SceneSwitch sceneSwitch,
        FirstLevelButtonView firstLevelButtonView)
    {
        _sceneSwitch = sceneSwitch;
        _firstLevelButtonView = firstLevelButtonView;
    }

    public override void Initialize()
    {
        _firstLevelButtonView.Clicked
            .Subscribe(_ => _sceneSwitch.LoadLevelAsync(2).Forget())
            .AddTo(CompositeDisposable);
    }
}
