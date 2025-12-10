using VContainer;
using VContainer.Unity;

public class ScoreCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ScoreModelBlockDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScoreModelPigDestroyerMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PointsObjectPoolBlockDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PointsObjectPoolPigDestroyerMediator>(Lifetime.Singleton);
    }
}
