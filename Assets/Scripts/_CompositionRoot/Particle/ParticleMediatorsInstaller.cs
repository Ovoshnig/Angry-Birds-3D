using VContainer;
using VContainer.Unity;

public class ParticleMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<TrailParticlePlayerBirdFlyerMediator>();
        builder.RegisterEntryPoint<TrailParticlePlayerSplitInto3BirdPowerMediator>();
        builder.RegisterEntryPoint<TrailParticlePlayerBirdPowerActivatorMediator>();
    }
}
