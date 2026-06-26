using VContainer;
using VContainer.Unity;

public class ParticleMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<TrailParticlePlayerBirdFlyerMediator>();
            entryPoints.Add<TrailParticlePlayerSplitInto3BirdPowerMediator>();
            entryPoints.Add<TrailParticlePlayerBirdPowerActivatorMediator>();

            entryPoints.Add<FeatherParticleViewObjectColliderMediator>();
            entryPoints.Add<FeatherParticleViewBirdDestroyerMediator>();
        });
    }
}
