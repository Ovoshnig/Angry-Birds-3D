using R3;

namespace Ovoshnig.Window.Window
{
    public interface IWindow
    {
        public bool TryOpen();
        public bool TryClose();
        public ReadOnlyReactiveProperty<bool> IsOpen { get; }
    }
}
