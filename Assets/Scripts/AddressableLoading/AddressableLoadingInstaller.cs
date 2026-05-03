using VContainer;
using VContainer.Unity;

public class AddressableLoadingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.Register<AddressableLoader>(Lifetime.Singleton);
}
