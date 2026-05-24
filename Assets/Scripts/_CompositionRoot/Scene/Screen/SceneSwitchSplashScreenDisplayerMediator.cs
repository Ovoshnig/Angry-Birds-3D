using Cysharp.Threading.Tasks;
using R3;

public class SceneSwitchSplashScreenDisplayerMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly SplashScreenDisplayer _splashScreenDisplayer;

    public SceneSwitchSplashScreenDisplayerMediator(SceneSwitch sceneSwitch,
        SplashScreenDisplayer splashScreenDisplayer)
    {
        _sceneSwitch = sceneSwitch;
        _splashScreenDisplayer = splashScreenDisplayer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _splashScreenDisplayer.IsPlaying
            .Pairwise()
            .Where(isPlaying => isPlaying.Previous && !isPlaying.Current)
            .Subscribe(_ => _sceneSwitch.LoadSceneAsync(SceneNavigationType.MainMenu).Forget())
            .AddTo(disposables);
    }
}
