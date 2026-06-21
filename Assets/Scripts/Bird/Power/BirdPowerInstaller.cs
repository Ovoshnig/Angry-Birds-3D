using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdPowerInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<IBirdPower, NoneBirdPower>(Lifetime.Singleton);
        builder.Register<IBirdPower, SplitInto3BirdPower>(Lifetime.Singleton);
        builder.Register<BirdPowerRegistry>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BirdPowerActivator>().AsSelf();
    }
}
