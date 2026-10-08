using R3;

namespace Ovoshnig.Cursor.State
{
    public sealed class CursorStateModel
    {
        private readonly ReactiveProperty<CursorState> _currentState = new(CursorState.UIHover);

        public ReadOnlyReactiveProperty<CursorState> CurrentState => _currentState;

        public void SetState(CursorState state) => _currentState.Value = state;
    }
}
