using R3;

public class WindowInputProvider : InputProvider<InputActions.WindowActions>
{
    public WindowInputProvider(InputActions inputActions) : base(inputActions.Window)
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        TogglePauseMenuPressed = ObserveButton(a => a.TogglePauseMenu);
    }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; }
    public ReadOnlyReactiveProperty<bool> TogglePauseMenuPressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
