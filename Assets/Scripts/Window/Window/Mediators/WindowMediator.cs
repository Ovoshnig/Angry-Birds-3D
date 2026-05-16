using R3;

public class WindowMediator : Mediator
{
    private readonly Window _window;
    private readonly WindowView _windowView;

    public WindowMediator(Window window, WindowView windowView)
    {
        _window = window;
        _windowView = windowView;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _window.IsOpen
            .Subscribe(_windowView.SetActive)
            .AddTo(disposables);

        _windowView.IsActive
            .Subscribe(_window.SetWindowActive)
            .AddTo(disposables);
    }
}
