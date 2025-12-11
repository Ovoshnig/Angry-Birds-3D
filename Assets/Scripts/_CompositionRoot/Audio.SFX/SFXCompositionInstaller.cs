using VContainer;
using VContainer.Unity;

public class SFXCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SFXPlayerObjectPoolBlockDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SFXPlayerObjectPoolPigDestroyerMediator>(Lifetime.Singleton);
    }
}
