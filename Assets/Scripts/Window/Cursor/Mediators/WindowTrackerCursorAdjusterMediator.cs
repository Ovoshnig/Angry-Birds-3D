using R3;

public class WindowTrackerCursorAdjusterMediator : Mediator
{
    private readonly WindowTracker _windowTracker;
    private readonly CursorAdjuster _cursorAdjuster;

    public WindowTrackerCursorAdjusterMediator(WindowTracker windowTracker, CursorAdjuster cursorAdjuster)
    {
        _windowTracker = windowTracker;
        _cursorAdjuster = cursorAdjuster;
    }

    public override void Start()
    {
        _windowTracker.IsOpen
            .Subscribe(isOpen =>
            {
                if (isOpen)
                    _cursorAdjuster.ShowCursor();
                else
                    _cursorAdjuster.HideCursor();
            })
            .AddTo(Disposables);
    }
}
