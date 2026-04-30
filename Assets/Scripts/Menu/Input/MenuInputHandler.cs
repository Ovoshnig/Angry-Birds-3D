using R3;
using VContainer.Unity;

public class MenuInputHandler : InputHandler<InputActions.MenuActions>, IInitializable
{
    public MenuInputHandler(InputActions inputActions)
        : base(inputActions.Menu) { }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; private set; }

    public void Initialize()
    {
        CloseCurrentPressed = BindButton(a => a.CloseCurrent);
        SkipTextPrintingPressed = BindButton(a => a.SkipTextPrinting);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
