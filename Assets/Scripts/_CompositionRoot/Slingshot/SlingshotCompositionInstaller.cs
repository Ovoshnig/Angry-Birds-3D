using VContainer;
using VContainer.Unity;

public class SlingshotCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<SlingshotShooterBirdQueueMediator>(Lifetime.Singleton);
}
