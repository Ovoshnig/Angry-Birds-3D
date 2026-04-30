using R3;
using VContainer.Unity;

public class ScreenInputHandler : InputHandler<InputActions.ScreenActions>, IInitializable
{
    public ScreenInputHandler(InputActions inputActions)
        : base(inputActions.Screen) { }

    public ReadOnlyReactiveProperty<bool> SwitchFullScreenPressed { get; private set; }
    public ReadOnlyReactiveProperty<bool> SkipSplashImagePressed { get; private set; }

    public void Initialize()
    {
        SwitchFullScreenPressed = BindButton(a => a.SwitchFullScreen);
        SkipSplashImagePressed = BindButton(a => a.SkipSplashImage);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
