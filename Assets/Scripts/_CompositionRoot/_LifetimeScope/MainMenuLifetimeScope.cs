using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;
    [SerializeField] private RatingShowingInstaller _ratingShowingInstaller;
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private PanelSwitchingInstaller _panelSwitchingInstaller;
    [SerializeField] private SocialLinkInstaller _socialLinkInstaller;
    [SerializeField] private DataStorageResetInstaller _dataStorageResetInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;
    [SerializeField] private ScreenInstaller _screenInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        _sceneSwitchingInstaller.Install(builder);
        _ratingShowingInstaller.Install(builder);
        _gameQuittingInstaller.Install(builder);
        _panelSwitchingInstaller.Install(builder);
        _socialLinkInstaller.Install(builder);
        _dataStorageResetInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
        _screenInstaller.Install(builder);
    }
}
