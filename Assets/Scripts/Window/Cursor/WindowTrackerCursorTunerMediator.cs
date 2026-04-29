using R3;

public class WindowTrackerCursorTunerMediator : Mediator
{
    private readonly WindowTracker _windowTracker;
    private readonly CursorTuner _cursorTuner;

    public WindowTrackerCursorTunerMediator(WindowTracker windowTracker, CursorTuner cursorTuner)
    {
        _windowTracker = windowTracker;
        _cursorTuner = cursorTuner;
    }

    public override void Start()
    {
        _windowTracker.IsOpen
            .Subscribe(isOpen =>
            {
                if (isOpen)
                    _cursorTuner.ShowCursor();
                else
                    _cursorTuner.HideCursor();
            })
            .AddTo(CompositeDisposable);
    }
}
