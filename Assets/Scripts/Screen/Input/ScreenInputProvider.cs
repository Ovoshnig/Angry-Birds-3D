using R3;

public class ScreenInputProvider : InputProvider<InputActions.ScreenActions>
{
    public ScreenInputProvider(InputActions inputActions) : base(inputActions.Screen)
    {
        SwitchFullScreenPressed = ObserveButton(a => a.SwitchFullScreen);
        SkipSplashImagePressed = ObserveButton(a => a.SkipSplashImage);
    }

    public ReadOnlyReactiveProperty<bool> SwitchFullScreenPressed { get; }
    public ReadOnlyReactiveProperty<bool> SkipSplashImagePressed { get; }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
