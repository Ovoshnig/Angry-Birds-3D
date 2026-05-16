using R3;

public class UIInputProvider : InputProvider<InputActions.UIActions>
{
    public UIInputProvider(InputActions inputActions) : base(inputActions.UI)
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        SkipTextPrintingPressed = ObserveButton(a => a.SkipTextPrinting);
    }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; }
    public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
