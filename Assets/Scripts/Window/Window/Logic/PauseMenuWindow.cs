using R3;

public class PauseMenuWindow : Window
{
    public PauseMenuWindow(WindowInputProvider windowInputProvider, WindowTracker windowTracker) 
        : base(windowInputProvider, windowTracker)
    {
    }

    protected override ReadOnlyReactiveProperty<bool> WindowSwitchPressed => 
        WindowInputProvider.PauseMenuSwitchPressed;
}
