using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class VSyncAdjuster : IPostInitializable, IStartable, IDisposable
{
    private readonly SettingsStorage _settingsStorage;
    private readonly ReactiveProperty<bool> _isVSyncEnabled = new();
    private readonly CompositeDisposable _disposables = new();

    public VSyncAdjuster(SettingsStorage settingsStorage) => _settingsStorage = settingsStorage;

    public ReadOnlyReactiveProperty<bool> IsVSyncEnabled => _isVSyncEnabled;

    public void PostInitialize()
    {
        _isVSyncEnabled.Value = _settingsStorage.Get(SettingsConstants.VSyncKey, false);
        QualitySettings.vSyncCount = _isVSyncEnabled.Value ? 1 : 0;
        Application.targetFrameRate = -1;
    }

    public void Start()
    {
        _settingsStorage.ResetHappened
            .Subscribe(_ => DisableVSync())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _settingsStorage.Set(SettingsConstants.VSyncKey, _isVSyncEnabled.Value);

        _disposables.Dispose();
        _isVSyncEnabled.Dispose();
    }

    public void SwitchVSync()
    {
        if (IsVSyncEnabled.CurrentValue)
            DisableVSync();
        else
            EnableVSync();
    }

    public void EnableVSync()
    {
        QualitySettings.vSyncCount = 1;
        _isVSyncEnabled.Value = true;
    }

    public void DisableVSync()
    {
        QualitySettings.vSyncCount = 0;
        _isVSyncEnabled.Value = false;
    }
}
