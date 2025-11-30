using VContainer;
using VContainer.Unity;

public class LevelStateCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<LevelStateSlingshotShooterMediator>(Lifetime.Singleton);
}

