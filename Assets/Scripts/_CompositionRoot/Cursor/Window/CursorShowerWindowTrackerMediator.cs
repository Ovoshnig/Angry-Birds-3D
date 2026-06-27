using R3;

public class CursorShowerWindowTrackerMediator : Mediator
{
    private readonly CursorShower _cursorShower;
    private readonly WindowTracker _windowTracker;

    public CursorShowerWindowTrackerMediator(CursorShower cursorShower, WindowTracker windowTracker)
    {
        _cursorShower = cursorShower;
        _windowTracker = windowTracker;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _windowTracker.IsOpen
            .Subscribe(isOpen =>
            {
                if (isOpen)
                    _cursorShower.ShowCursor();
                else
                    _cursorShower.HideCursor();
            })
            .AddTo(disposables);
    }
}
