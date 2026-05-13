using VContainer;
using VContainer.Unity;

public class SlingshotMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SlingshotShooterStartCameraSwitchMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SlingshotShooterBirdDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SlingshotShooterLevelTrackerMediator>(Lifetime.Singleton);
    }
}
