using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private DataStorageInstaller _dataStorageInstaller;
    [SerializeField] private GameSettingsInstaller _gameSettingsInstaller;
    [SerializeField] private SceneLoadingScreenInstaller _sceneLoadingScreenInstaller;
    [SerializeField] private CursorInstaller _cursorInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputActions>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<ScreenInputProvider>().AsSelf();
            entryPoints.Add<UIInputProvider>().AsSelf();
            entryPoints.Add<SceneSwitch>().AsSelf();
        });

        new AddressableLoadingInstaller().Install(builder);

        _dataStorageInstaller.Install(builder);
        _gameSettingsInstaller.Install(builder);
        _sceneLoadingScreenInstaller.Install(builder);
        _cursorInstaller.Install(builder);

        new InputActionsMediatorsInstaller().Install(builder);
    }
}
