using Ovoshnig.UI.Basic;
using Ovoshnig.Window.Window;
using R3;

namespace Ovoshnig.Window.Pause;

public class PauseMenuWindowButtonViewMediator(PauseMenuWindow pauseMenuWindow, PauseButtonView view)
    : UIViewMediator<PauseButtonView>(view)
{
    private readonly PauseMenuWindow _pauseMenuWindow = pauseMenuWindow;

    protected override void OnViewEnabled(PauseButtonView view, CompositeDisposable viewDisposables)
    {
        _pauseMenuWindow.IsOpen
            .Subscribe(isOpen => view.SetInteractable(!isOpen))
            .AddTo(viewDisposables);

        view.Clicked
            .Subscribe(_ => _pauseMenuWindow.TryOpen())
            .AddTo(viewDisposables);
    }
}
