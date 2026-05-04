using VContainer;
using VContainer.Unity;

public class DataStorageInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SaveStorage>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<SettingsStorage>(Lifetime.Singleton).AsSelf();
    }
}
