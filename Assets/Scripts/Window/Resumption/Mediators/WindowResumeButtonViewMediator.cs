using R3;

public class WindowResumeButtonViewMediator : UIMediator<ResumeButtonView>
{
    private readonly Window _window;

    public WindowResumeButtonViewMediator(Window window, ResumeButtonView view)
        : base(view) => _window = window;

    protected override void OnViewEnabled(ResumeButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _window.TryClose())
            .AddTo(viewDisposables);
    }
}
