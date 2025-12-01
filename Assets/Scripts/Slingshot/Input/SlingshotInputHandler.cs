using R3;

public class SlingshotInputHandler : InputHandler<InputActions.SlingshotActions>
{
    public SlingshotInputHandler(InputActions inputActions) : base(inputActions.Slingshot)
    { }

    public ReadOnlyReactiveProperty<bool> LeftButtonPressed { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        LeftButtonPressed = BindButton(a => a.LeftButton);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
