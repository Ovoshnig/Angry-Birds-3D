using VContainer;
using VContainer.Unity;

public class ScoreMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ScoreModelObjectDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PointsPoolObjectDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScoreViewLevelTrackerMediator>(Lifetime.Singleton);
    }
}
