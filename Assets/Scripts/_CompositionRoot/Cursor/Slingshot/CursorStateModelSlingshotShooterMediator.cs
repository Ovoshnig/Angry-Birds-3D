using AngryBirds3D.Slingshot.Shooting;
using Ovoshnig.Cursor.State;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class CursorStateModelSlingshotShooterMediator(CursorStateModel cursorStateModel,
    SlingshotShooter slingshotShooter) : Mediator
{
    private readonly CursorStateModel _cursorStateModel = cursorStateModel;
    private readonly SlingshotShooter _slingshotShooter = slingshotShooter;

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
