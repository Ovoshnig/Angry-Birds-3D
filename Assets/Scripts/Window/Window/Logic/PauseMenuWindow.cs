using Ovoshnig.Window.Input;
using R3;

namespace Ovoshnig.Window.Window;

public class PauseMenuWindow(WindowInputProvider inputProvider, WindowTracker windowTracker)
    : Window(inputProvider, windowTracker)
{
    protected override ReadOnlyReactiveProperty<bool> GetToggleWindowPressedProperty(
        WindowInputProvider inputProvider) => inputProvider.TogglePauseMenuPressed;
}
