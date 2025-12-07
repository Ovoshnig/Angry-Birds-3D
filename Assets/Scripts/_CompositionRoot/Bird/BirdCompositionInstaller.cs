using VContainer;
using VContainer.Unity;

public class BirdCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BirdFlyerSlingshotShooterMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BirdSFXPlayerSlingshotShooterMediator>(Lifetime.Singleton);
    }
}

