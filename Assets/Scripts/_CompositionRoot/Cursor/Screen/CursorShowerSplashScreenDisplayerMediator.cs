using Ovoshnig.Cursor.Showing;
using Ovoshnig.Mediation;
using Ovoshnig.Screen.SplashScreen;
using R3;

namespace AngryBirds3D.Composition;

public class CursorShowerSplashScreenDisplayerMediator(SplashScreenDisplayer splashScreenDisplayer)
    : Mediator
{
    private readonly SplashScreenDisplayer _splashScreenDisplayer = splashScreenDisplayer;

    protected override void Bind(CompositeDisposable disposables)
    {
        _splashScreenDisplayer.IsPlaying
            .Subscribe(isPlaying => CursorShower.SetShowing(!isPlaying))
            .AddTo(disposables);
    }
}
