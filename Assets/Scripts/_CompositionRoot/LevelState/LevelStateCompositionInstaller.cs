using VContainer;
using VContainer.Unity;

public class LevelStateCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<LevelStateBirdFlyerMediator>(Lifetime.Singleton);
}

