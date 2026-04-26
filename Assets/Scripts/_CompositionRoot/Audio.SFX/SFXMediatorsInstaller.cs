using VContainer;
using VContainer.Unity;

public class SFXMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SFXPlayerPoolSlingshotShooterMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SFXPlayerPoolBirdColliderMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SFXPlayerPoolObjectDestroyerMediator<BlockEntityView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SFXPlayerPoolObjectDestroyerMediator<PigEntityView>>(Lifetime.Singleton);
    }
}
