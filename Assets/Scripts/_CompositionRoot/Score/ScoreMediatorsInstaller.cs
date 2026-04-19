using VContainer;
using VContainer.Unity;

public class ScoreMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ScoreModelObjectDestroyerMediator<BlockEntityView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ScoreModelObjectDestroyerMediator<PigEntityView>>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PointsPoolObjectDestroyerMediator<BlockEntityView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PointsPoolObjectDestroyerMediator<PigEntityView>>(Lifetime.Singleton);
    }
}
