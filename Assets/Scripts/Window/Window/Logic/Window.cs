using R3;
using System;
using VContainer.Unity;

public abstract class Window : IWindow, IStartable, IDisposable
{
    private readonly WindowInputProvider _windowInputProvider;
    private readonly WindowTracker _windowTracker;
    private readonly ReactiveProperty<bool> _isOpen = new(false);
    private readonly CompositeDisposable _disposables = new();

    private bool _isWindowActive = false;

    public Window(WindowInputProvider windowInputProvider, WindowTracker windowTracker)
    {
        _windowInputProvider = windowInputProvider;
        _windowTracker = windowTracker;
    }

    public ReadOnlyReactiveProperty<bool> IsOpen => _isOpen;

    protected abstract ReadOnlyReactiveProperty<bool> WindowSwitchPressed { get; }
    protected WindowInputProvider WindowInputProvider => _windowInputProvider;

    public virtual void Start()
    {
        WindowSwitchPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => OnWindowSwitchPressed())
            .AddTo(_disposables);

        WindowInputProvider.CloseCurrentPressed
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

    protected virtual void OnWindowSwitchPressed()
    {
        if (IsOpen.CurrentValue)
            TryClose();
        else
            TryOpen();
    }
}
