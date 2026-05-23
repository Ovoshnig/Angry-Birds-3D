using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private SettingsStorageResetInstaller _settingsStorageResetInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;
    [SerializeField] private ScreenSettingsInstaller _screenSettingsInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        _gameQuittingInstaller.Install(builder);
        _settingsStorageResetInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
        _screenSettingsInstaller.Install(builder);
    }
}
