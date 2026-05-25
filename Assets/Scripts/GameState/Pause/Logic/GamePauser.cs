using R3;
using System;
using UnityEngine;

public class GamePauser : IDisposable
{
    private readonly ReactiveProperty<bool> _isPaused = new(false);

    public ReadOnlyReactiveProperty<bool> IsPaused => _isPaused;

    public void Dispose() => _isPaused.Dispose();

    public void TogglePause() => SetPause(!_isPaused.Value);

    public void SetPause(bool isPaused)
    {
        _isPaused.Value = isPaused;

        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
