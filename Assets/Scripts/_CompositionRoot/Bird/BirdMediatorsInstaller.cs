using VContainer;
using VContainer.Unity;

public class BirdMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<BirdFlyerSlingshotShooterMediator>();
            entryPoints.Add<BirdPointsDisplayerLevelTrackerMediator>();
        });
    }
}
