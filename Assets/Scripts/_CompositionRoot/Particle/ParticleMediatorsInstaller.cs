using VContainer;
using VContainer.Unity;

public class ParticleMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<FeatherParticleViewObjectColliderMediator>();
            entryPoints.Add<FeatherParticleViewBirdDestroyerMediator>();
        });
    }
}
