using VContainer;
using VContainer.Unity;

public class BirdCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdQueueLevelStateMediator>(Lifetime.Singleton);
}

