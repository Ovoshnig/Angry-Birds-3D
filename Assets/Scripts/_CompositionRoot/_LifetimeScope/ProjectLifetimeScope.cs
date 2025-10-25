using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettingsInstaller _gameSettingsInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ScreenInputHandler>(Lifetime.Singleton)
            .AsSelf();

        new InputActionsInstaller().Install(builder);
        new AddressableLoadingInstaller().Install(builder);
        new SceneInstaller().Install(builder);

        _gameSettingsInstaller.Install(builder);
    }
}
