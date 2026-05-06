using R3;
using VContainer.Unity;

public class MenuInputHandler : InputProvider<InputActions.MenuActions>, IInitializable
{
    public MenuInputHandler(InputActions inputActions)
        : base(inputActions.Menu) { }

    public ReadOnlyReactiveProperty<bool> CloseCurrentPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> SkipTextPrintingPressed { get; private set; }

    public void Initialize()
    {
        CloseCurrentPressed = ObserveButton(a => a.CloseCurrent);
        SkipTextPrintingPressed = ObserveButton(a => a.SkipTextPrinting);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
