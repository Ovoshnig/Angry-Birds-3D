using VContainer;
using VContainer.Unity;

public class SlingshotMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<SlingshotShooterBirdQueueMediator>(Lifetime.Singleton);
}
