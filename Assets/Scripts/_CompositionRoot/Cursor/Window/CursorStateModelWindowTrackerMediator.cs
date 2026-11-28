using Ovoshnig.Cursor.State;
using Ovoshnig.Mediation;
using Ovoshnig.Window.Window;
using R3;

namespace AngryBirds3D.Composition;

public class CursorStateModelWindowTrackerMediator(CursorStateModel cursorStateModel,
    WindowTracker windowTracker) : Mediator
{
    private readonly CursorStateModel _cursorStateModel = cursorStateModel;
    private readonly WindowTracker _windowTracker = windowTracker;

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
