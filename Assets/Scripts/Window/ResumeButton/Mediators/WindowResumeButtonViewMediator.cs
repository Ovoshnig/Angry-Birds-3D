using R3;

public class WindowResumeButtonViewMediator : UIMediator<ResumeButtonView>
{
    private readonly Window _window;
    private readonly ResumeButtonView _resumeButtonView;

    public WindowResumeButtonViewMediator(Window window, ResumeButtonView resumeButtonView)
        : base(resumeButtonView)
    {
        _window = window;
        _resumeButtonView = resumeButtonView;
    }

    protected override void OnViewEnabled()
    {
        _resumeButtonView.Clicked
            .Subscribe(_ => _window.TryClose())
            .AddTo(Disposables);
    }
}
