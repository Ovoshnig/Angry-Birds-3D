using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private PanelCloseButtonsInstaller _panelCloseButtonsInstaller;
    [SerializeField] private SettingsStorageResetInstaller _settingsStorageResetInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;
    [SerializeField] private ScreenSettingsInstaller _screenSettingsInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<UIInputProvider>().AsSelf();
            entryPoints.Add<SaveStorageSceneButtonViewsMediator>();
        });

        _sceneSwitchingInstaller.Install(builder);
        _gameQuittingInstaller.Install(builder);
        _panelCloseButtonsInstaller.Install(builder);
        _settingsStorageResetInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
        _screenSettingsInstaller.Install(builder);
    }
}
