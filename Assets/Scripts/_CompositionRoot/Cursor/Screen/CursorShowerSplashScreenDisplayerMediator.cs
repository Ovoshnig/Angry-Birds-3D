using R3;

public class CursorShowerSplashScreenDisplayerMediator : Mediator
{
    private readonly CursorShower _cursorShower;
    private readonly SplashScreenDisplayer _splashScreenDisplayer;

    public CursorShowerSplashScreenDisplayerMediator(CursorShower cursorShower,
        SplashScreenDisplayer splashScreenDisplayer)
    {
        _cursorShower = cursorShower;
        _splashScreenDisplayer = splashScreenDisplayer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _splashScreenDisplayer.IsPlaying
            .Subscribe(isPlaying => _cursorShower.SetShowing(!isPlaying))
            .AddTo(disposables);
    }
}
