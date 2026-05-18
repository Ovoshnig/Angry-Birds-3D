using VContainer;
using VContainer.Unity;

public class ScoreMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<PointsPoolObjectDestroyerMediator>();
            entryPoints.Add<PointsPoolBirdDisplayerMediator>();
            entryPoints.Add<ScoreViewCompletionPanelsMediator>();
        });
    }
}
