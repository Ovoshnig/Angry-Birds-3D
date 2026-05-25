using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class VSyncAdjuster : IPostInitializable, IStartable, IDisposable
{
    private readonly SettingsStorage _settingsStorage;
    private readonly ReactiveProperty<bool> _isVSync = new();
    private readonly CompositeDisposable _disposables = new();

    public VSyncAdjuster(SettingsStorage settingsStorage) => _settingsStorage = settingsStorage;

    public ReadOnlyReactiveProperty<bool> IsVSync => _isVSync;

    public void PostInitialize()
    {
        SetVSync(_settingsStorage.Get(SettingsConstants.VSyncKey, false));
        Application.targetFrameRate = -1;
    }

    public void Start()
    {
        _settingsStorage.ResetHappened
            .Subscribe(_ => SetVSync(false))
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _settingsStorage.Set(SettingsConstants.VSyncKey, _isVSync.Value);

        _disposables.Dispose();
        _isVSync.Dispose();
    }

    public void ToggleVSync() => SetVSync(!_isVSync.Value);

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
        _isVSync.Value = isVSync;
    }
}
