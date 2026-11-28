using Ovoshnig.Window.Input;
using R3;
using System;
using VContainer.Unity;

namespace Ovoshnig.Window.Window;

public abstract class Window(WindowInputProvider inputProvider, WindowTracker windowTracker) : IWindow,
    IStartable, IDisposable
{
    private readonly WindowInputProvider _inputProvider = inputProvider;
    private readonly WindowTracker _windowTracker = windowTracker;
    private readonly ReactiveProperty<bool> _isOpen = new(false);
    private readonly CompositeDisposable _disposables = [];

    private bool _isWindowActive = false;

    public ReadOnlyReactiveProperty<bool> IsOpen => _isOpen;

    public virtual void Start()
    {
        GetToggleWindowPressedProperty(_inputProvider)
            .Where(isPressed => isPressed)
            .Subscribe(_ => Toggle())
            .AddTo(_disposables);

        _inputProvider.CloseCurrentPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => TryClose())
            .AddTo(_disposables);
    }

    public virtual void Dispose()
    {
        _disposables.Dispose();

        _isOpen.Dispose();
    }

    public virtual bool TryOpen()
    {
        if (!_windowTracker.TryOpenWindow(this))
            return false;

        _isOpen.Value = true;
        return true;
    }

    public virtual bool TryClose()
    {
        if (!_isWindowActive || !_windowTracker.TryCloseWindow())
            return false;

        _isOpen.Value = false;
        return true;
    }

    public void SetWindowActive(bool value) => _isWindowActive = value;

    public void Toggle()
    {
        if (IsOpen.CurrentValue)
            TryClose();
        else
            TryOpen();
    }

    public void StopToggling()
    {
        _disposables.Clear();
        TryClose();
    }

    protected abstract ReadOnlyReactiveProperty<bool> GetToggleWindowPressedProperty(
        WindowInputProvider inputProvider);
}
