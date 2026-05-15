using R3;

public class UIInputProvider : InputProvider<InputActions.MenuActions>
{
    public UIInputProvider(InputActions inputActions) : base(inputActions.Menu)
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        SkipTextPrintingPressed = ObserveButton(a => a.SkipTextPrinting);
    }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; }
    public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
