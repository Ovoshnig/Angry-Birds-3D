using VContainer;
using VContainer.Unity;

public class ScoreMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PointsPoolObjectDestroyerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PointsPoolBirdDisplayerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScoreViewBirdDisplayerMediator>(Lifetime.Singleton);
    }
}
