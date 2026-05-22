using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettingsInstaller _gameSettingsInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputActions>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScreenInputProvider>().AsSelf();

        new AddressableLoadingInstaller().Install(builder);

        _gameSettingsInstaller.Install(builder);
    }
}
