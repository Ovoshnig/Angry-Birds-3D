using R3;

public class MenuInputHandler : InputHandler<InputActions.MenuActions>
{
    public MenuInputHandler(InputActions inputActions)
        : base(inputActions.Menu) { }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        CloseCurrentPressed = BindButton(a => a.CloseCurrent);
        SkipTextPrintingPressed = BindButton(a => a.SkipTextPrinting);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
