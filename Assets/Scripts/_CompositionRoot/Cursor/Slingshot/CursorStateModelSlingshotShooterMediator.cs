using R3;

public class CursorStateModelSlingshotShooterMediator : Mediator
{
    private readonly CursorStateModel _cursorStateModel;
    private readonly SlingshotShooter _slingshotShooter;

    public CursorStateModelSlingshotShooterMediator(CursorStateModel cursorStateModel,
        SlingshotShooter slingshotShooter)
    {
        _cursorStateModel = cursorStateModel;
        _slingshotShooter = slingshotShooter;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _slingshotShooter.CurrentState
            .Subscribe(state =>
            {
                if (state == SlingshotState.Dragging)
                    _cursorStateModel.SetState(CursorState.GameplayGrab);
                else if (state == SlingshotState.Idle)
                    _cursorStateModel.SetState(CursorState.GameplayHover);
            })
            .AddTo(disposables);
    }
}
