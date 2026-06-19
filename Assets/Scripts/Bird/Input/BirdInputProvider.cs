using R3;

public class BirdInputProvider : InputProvider<InputActions.BirdActions>
{
    public BirdInputProvider(InputActions.BirdActions actions) : base(actions) =>
        UsePowerPressed = ObserveButton(a => a.UsePower);

    public ReadOnlyReactiveProperty<bool> UsePowerPressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
