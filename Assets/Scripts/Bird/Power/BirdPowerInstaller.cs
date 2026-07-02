using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdPowerInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<SplitInto3BirdPower>(Lifetime.Singleton).As<IBirdPower>().AsSelf();
        builder.Register<BoostBirdPower>(Lifetime.Singleton).As<IBirdPower>().AsSelf();
        builder.Register<ExplosionBirdPower>(Lifetime.Singleton).As<IBirdPower>().AsSelf();

        builder.Register<BirdPowerRegistry>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<BirdPowerActivator>().AsSelf();
            entryPoints.Add<BirdPowerActivatorBirdFlyerMediator>().AsSelf();
        });
    }
}
