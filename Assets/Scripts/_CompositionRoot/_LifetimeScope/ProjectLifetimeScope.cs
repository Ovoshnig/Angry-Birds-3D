using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private DataStorageInstaller _dataStorageInstaller;
    [SerializeField] private GameSettingsInstaller _gameSettingsInstaller;
    [SerializeField] private SceneLoadingScreenInstaller _sceneLoadingScreenInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputActions>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScreenInputProvider>().AsSelf();

        builder.RegisterEntryPoint<SceneSwitch>().AsSelf();

        new AddressableLoadingInstaller().Install(builder);

        _dataStorageInstaller.Install(builder);
        _gameSettingsInstaller.Install(builder);
        _sceneLoadingScreenInstaller.Install(builder);

        new InputActionsMediatorsInstaller().Install(builder);
    }
}
