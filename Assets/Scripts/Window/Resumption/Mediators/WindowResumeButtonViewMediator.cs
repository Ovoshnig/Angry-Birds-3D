using Ovoshnig.UI.Basic;
using R3;
using WindowBase = Ovoshnig.Window.Window.Window;

namespace Ovoshnig.Window.Resumption
{
    public class WindowResumeButtonViewMediator : UIViewMediator<ResumeButtonView>
    {
        private readonly WindowBase _window;

        public WindowResumeButtonViewMediator(WindowBase window, ResumeButtonView view)
            : base(view) => _window = window;

        protected override void OnViewEnabled(ResumeButtonView view, CompositeDisposable viewDisposables)
        {
            view.Clicked
                .Subscribe(_ => _window.TryClose())
                .AddTo(viewDisposables);
        }
    }
}
