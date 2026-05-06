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

        _screenInputProvider.SwitchFullScreenPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => OnSwitchFullScreenPressed())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _isFullScreen.Dispose();
    }

    public void OnSwitchFullScreenPressed()
    {
        if (IsFullScreen.CurrentValue)
            DisableFullScreen();
        else
            EnableFullScreen();
    }

    public void EnableFullScreen()
    {
        Screen.fullScreen = true;
        _isFullScreen.Value = true;
    }

    public void DisableFullScreen()
    {
        Screen.fullScreen = false;
        _isFullScreen.Value = false;
    }
}
