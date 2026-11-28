using Ovoshnig.Cursor.Showing;
using Ovoshnig.Mediation;
using Ovoshnig.Screen.SplashScreen;
using R3;

namespace AngryBirds3D.Composition;

public class CursorShowerSplashScreenDisplayerMediator : Mediator
{
    private readonly SplashScreenDisplayer _splashScreenDisplayer;

    public CursorShowerSplashScreenDisplayerMediator(SplashScreenDisplayer splashScreenDisplayer) =>
        _splashScreenDisplayer = splashScreenDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _splashScreenDisplayer.IsPlaying
            .Subscribe(isPlaying => CursorShower.SetShowing(!isPlaying))
            .AddTo(disposables);
    }
}
