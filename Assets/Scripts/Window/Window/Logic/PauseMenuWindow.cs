using R3;

public class PauseMenuWindow : Window
{
    public PauseMenuWindow(WindowInputProvider inputProvider, WindowTracker windowTracker)
        : base(inputProvider, windowTracker)
    {
    }

    protected override ReadOnlyReactiveProperty<bool> GetToggleWindowPressedProperty(WindowInputProvider inputProvider) =>
        inputProvider.TogglePauseMenuPressed;
}
