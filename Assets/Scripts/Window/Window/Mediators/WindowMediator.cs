using Ovoshnig.Mediation;
using R3;

namespace Ovoshnig.Window.Window;

public class WindowMediator(Window window, WindowView windowView) : Mediator
{
    private readonly Window _window = window;
    private readonly WindowView _windowView = windowView;

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
