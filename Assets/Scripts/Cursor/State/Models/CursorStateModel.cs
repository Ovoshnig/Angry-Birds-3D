using R3;

public sealed class CursorStateModel
{
    private readonly ReactiveProperty<CursorState> _currentState = new(CursorState.UIHover);

    public ReadOnlyReactiveProperty<CursorState> CurrentState => _currentState;

    public void SetState(CursorState state) => _currentState.Value = state;
}
