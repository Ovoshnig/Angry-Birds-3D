using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;
    [SerializeField] private RatingShowingInstaller _ratingShowingInstaller;
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private PanelCloseButtonsInstaller _panelCloseButtonsInstaller;
    [SerializeField] private DataStorageResetInstaller _dataStorageResetInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;
    [SerializeField] private ScreenInstaller _screenInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<UIInputProvider>().AsSelf();

        _sceneSwitchingInstaller.Install(builder);
        _ratingShowingInstaller.Install(builder);
        _gameQuittingInstaller.Install(builder);
        _panelCloseButtonsInstaller.Install(builder);
        _dataStorageResetInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
        _screenInstaller.Install(builder);
    }
}
