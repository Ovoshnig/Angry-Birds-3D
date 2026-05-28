using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;
    [SerializeField] private RatingShowingInstaller _ratingShowingInstaller;
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private PanelCloseButtonsInstaller _panelCloseButtonsInstaller;
    [SerializeField] private SettingsStorageResetInstaller _settingsStorageResetInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;
    [SerializeField] private ScreenSettingsInstaller _screenSettingsInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<UIInputProvider>().AsSelf();

        _sceneSwitchingInstaller.Install(builder);
        _ratingShowingInstaller.Install(builder);
        _gameQuittingInstaller.Install(builder);
        _panelCloseButtonsInstaller.Install(builder);
        _settingsStorageResetInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
        _screenSettingsInstaller.Install(builder);
    }
}
