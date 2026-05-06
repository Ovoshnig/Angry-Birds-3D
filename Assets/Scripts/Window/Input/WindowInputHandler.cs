using R3;
using VContainer.Unity;

public class WindowInputHandler : InputProvider<InputActions.WindowActions>, IInitializable
{
    public WindowInputHandler(InputActions inputActions)
        : base(inputActions.Window) { }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> PauseMenuSwitchPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> InventorySwitchPressed { get; private set; }

    public void Initialize()
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        PauseMenuSwitchPressed = ObserveButton(a => a.SwitchPauseMenu);
        InventorySwitchPressed = ObserveButton(a => a.SwitchInventory);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
