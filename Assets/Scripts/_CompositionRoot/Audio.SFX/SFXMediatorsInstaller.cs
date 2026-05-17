using VContainer;
using VContainer.Unity;

public class SFXMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SFXPlayerLevelTrackerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SFXPlayerClearingPanelViewMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SFXPlayerPoolSlingshotShooterMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SFXPlayerPoolBirdColliderMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SFXPlayerPoolBirdDestroyerMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SFXPlayerPoolObjectDestroyerMediator>(Lifetime.Singleton);
    }
}
