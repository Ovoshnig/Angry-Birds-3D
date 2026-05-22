using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainMenuLifetimeScope : LifetimeScope
{
    [SerializeField] private GameQuittingInstaller _gameQuittingInstaller;
    [SerializeField] private AudioTuningInstaller _audioTuningInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        _gameQuittingInstaller.Install(builder);
        _audioTuningInstaller.Install(builder);
    }
}
