using VContainer;
using VContainer.Unity;

public class SlingshotMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SlingshotShooterStartCameraSwitchMediator>();
            entryPoints.Add<SlingshotShooterBirdDestroyerMediator>();
            entryPoints.Add<SlingshotShooterLevelTrackerMediator>();
        });
    }
}
