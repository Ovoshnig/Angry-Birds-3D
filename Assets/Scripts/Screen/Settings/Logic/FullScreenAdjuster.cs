using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class FullScreenAdjuster : IStartable, IDisposable
{
    private readonly ScreenInputHandler _screenInputHandler;
    private readonly ReactiveProperty<bool> _isFullScreen = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public FullScreenAdjuster(ScreenInputHandler screenInputHandler) => _screenInputHandler = screenInputHandler;

    public ReadOnlyReactiveProperty<bool> IsFullScreen => _isFullScreen;

    public void Start()
    {
        _isFullScreen.Value = Screen.fullScreen;

        _screenInputHandler.SwitchFullScreenPressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => OnSwitchFullScreenPressed())
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

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
