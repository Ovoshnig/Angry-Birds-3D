using R3;

public class WindowInputProvider : InputProvider<InputActions.WindowActions>
{
    public WindowInputProvider(InputActions inputActions) : base(inputActions.Window)
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        PauseMenuSwitchPressed = ObserveButton(a => a.SwitchPauseMenu);
        InventorySwitchPressed = ObserveButton(a => a.SwitchInventory);
    }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; }
    public ReadOnlyReactiveProperty<bool> PauseMenuSwitchPressed { get; }
    public ReadOnlyReactiveProperty<bool> InventorySwitchPressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
