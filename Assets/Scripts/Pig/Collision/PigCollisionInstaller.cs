using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<PigCollisionReporter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PigCollisionMediator>(Lifetime.Singleton);
    }
}
