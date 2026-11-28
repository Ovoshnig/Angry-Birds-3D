using Cysharp.Threading.Tasks;
using Ovoshnig.Mediation;
using Ovoshnig.Scene.Switching;
using Ovoshnig.Screen.SplashScreen;
using R3;

namespace AngryBirds3D.Composition;

public class SceneSwitchSplashScreenDisplayerMediator(SceneSwitch sceneSwitch,
    SplashScreenDisplayer splashScreenDisplayer) : Mediator
{
    private readonly SceneSwitch _sceneSwitch = sceneSwitch;
    private readonly SplashScreenDisplayer _splashScreenDisplayer = splashScreenDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _splashScreenDisplayer.IsPlaying
            .Pairwise()
            .Where(isPlaying => isPlaying.Previous && !isPlaying.Current)
            .Subscribe(_ => _sceneSwitch.LoadSceneAsync(SceneNavigationType.MainMenu).Forget())
            .AddTo(disposables);
    }
}
