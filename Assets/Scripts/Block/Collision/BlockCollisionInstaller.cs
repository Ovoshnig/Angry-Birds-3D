using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<BlockCollisionReporter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BlockCollisionMediator>(Lifetime.Singleton);
    }
}

