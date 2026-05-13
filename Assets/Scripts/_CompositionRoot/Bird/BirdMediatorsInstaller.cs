using VContainer;
using VContainer.Unity;

public class BirdMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdFlyerSlingshotShooterMediator>(Lifetime.Singleton);
}
