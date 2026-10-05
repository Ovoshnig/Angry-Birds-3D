using Ovoshnig.Window.Input;
using R3;

namespace Ovoshnig.Window.Window
{
    public class PauseMenuWindow : Window
    {
        public PauseMenuWindow(WindowInputProvider inputProvider, WindowTracker windowTracker)
            : base(inputProvider, windowTracker)
        {
        }

        protected override ReadOnlyReactiveProperty<bool> GetToggleWindowPressedProperty(WindowInputProvider inputProvider) =>
            inputProvider.TogglePauseMenuPressed;
    }
}
