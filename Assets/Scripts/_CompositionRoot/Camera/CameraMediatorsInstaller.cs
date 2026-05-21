using VContainer;
using VContainer.Unity;

public class CameraMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<CameraSwitchViewBirdFlyerMediator>();
            entryPoints.Add<CameraSwitchViewLevelTrackerMediator>();
        });
    }
}
