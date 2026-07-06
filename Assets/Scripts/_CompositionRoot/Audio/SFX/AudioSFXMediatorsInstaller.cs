using VContainer;
using VContainer.Unity;

public class AudioSFXMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SFXPlayerPoolLevelTrackerMediator>();
            entryPoints.Add<SFXPlayerPoolClearingPanelViewMediator>();
            entryPoints.Add<SFXPlayerPoolSlingshotShooterMediator>();
            entryPoints.Add<SFXPlayerPoolBirdColliderMediator>();
            entryPoints.Add<SFXPlayerPoolBirdDestroyerMediator>();
            entryPoints.Add<SFXPlayerPoolBirdPowerActivatorMediator>();
            entryPoints.Add<SFXPlayerPoolBirdExploderMediator>();
            entryPoints.Add<SFXPlayerPoolObjectDestroyerMediator>();
        });
    }
}
