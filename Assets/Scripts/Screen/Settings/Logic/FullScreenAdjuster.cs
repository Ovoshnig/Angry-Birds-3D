using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class FullScreenAdjuster : IStartable, IDisposable
{
    private readonly ScreenInputProvider _screenInputProvider;
    private readonly ReactiveProperty<bool> _isFullScreen = new();
    private readonly CompositeDisposable _disposables = new();

    public FullScreenAdjuster(ScreenInputProvider screenInputProvider) => _screenInputProvider = screenInputProvider;

    public ReadOnlyReactiveProperty<bool> IsFullScreen => _isFullScreen;

    public void Start()
    {
        _isFullScreen.Value = Screen.fullScreen;

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
        Screen.fullScreen = isFullScreen;
        _isFullScreen.Value = isFullScreen;
    }
}
