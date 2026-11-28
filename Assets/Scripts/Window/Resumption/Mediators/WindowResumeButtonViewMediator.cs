using Ovoshnig.UI.Basic;
using R3;
using WindowBase = Ovoshnig.Window.Window.Window;

namespace Ovoshnig.Window.Resumption;

public class WindowResumeButtonViewMediator(WindowBase window, ResumeButtonView view)
    : UIViewMediator<ResumeButtonView>(view)
{
    private readonly WindowBase _window = window;

    protected override void OnViewEnabled(ResumeButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _window.TryClose())
            .AddTo(viewDisposables);
    }
}
