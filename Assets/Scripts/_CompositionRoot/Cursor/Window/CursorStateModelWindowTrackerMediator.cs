using R3;

public class CursorStateModelWindowTrackerMediator : Mediator
{
    private readonly CursorStateModel _cursorStateModel;
    private readonly WindowTracker _windowTracker;

    public CursorStateModelWindowTrackerMediator(CursorStateModel cursorStateModel, WindowTracker windowTracker)
    {
        _cursorStateModel = cursorStateModel;
        _windowTracker = windowTracker;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _windowTracker.IsOpen
            .Subscribe(isOpen =>
            {
                if (isOpen)
                    _cursorStateModel.SetState(CursorState.UIHover);
                else
                    _cursorStateModel.SetState(CursorState.GameplayHover);
            })
            .AddTo(disposables);
    }
}
