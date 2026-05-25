using R3;

public class PauseMenuWindowButtonViewMediator : UIMediator<PauseButtonView>
{
    private readonly PauseMenuWindow _pauseMenuWindow;

    public PauseMenuWindowButtonViewMediator(PauseMenuWindow pauseMenuWindow, PauseButtonView view)
        : base(view) => _pauseMenuWindow = pauseMenuWindow;

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
