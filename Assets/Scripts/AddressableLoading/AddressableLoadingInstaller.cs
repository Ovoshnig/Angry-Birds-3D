using VContainer;
using VContainer.Unity;

namespace Ovoshnig.AddressableLoading
{
    public class AddressableLoadingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.Register<AddressableLoader>(Lifetime.Singleton);
    }
}
