using VContainer;
using VContainer.Unity;

public class SFXMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SFXPlayerObjectPoolBlockDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SFXPlayerObjectPoolPigDestroyerMediator>(Lifetime.Singleton);
    }
}
