using Ovoshnig.Screen.Input;
using R3;
using System;
using VContainer.Unity;
using UnityScreen = UnityEngine.Screen;

namespace Ovoshnig.Screen.FullScreenAdjustment;

public sealed class FullScreenAdjuster(ScreenInputProvider screenInputProvider) : IStartable, IDisposable
{
    private readonly ScreenInputProvider _screenInputProvider = screenInputProvider;
    private readonly ReactiveProperty<bool> _isFullScreen = new();
    private readonly CompositeDisposable _disposables = [];

    public ReadOnlyReactiveProperty<bool> IsFullScreen => _isFullScreen;

    public void Start()
    {
        _isFullScreen.Value = UnityScreen.fullScreen;

        _screenInputProvider.ToggleFullScreenPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => ToggleFullScreen())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _isFullScreen.Dispose();
    }

    public void ToggleFullScreen() => SetFullScreen(!_isFullScreen.Value);

    public void SetFullScreen(bool isFullScreen)
    {
        UnityScreen.fullScreen = isFullScreen;
        _isFullScreen.Value = isFullScreen;
    }
}
