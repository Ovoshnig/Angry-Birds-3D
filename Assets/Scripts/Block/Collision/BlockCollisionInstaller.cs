using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<BlockCollider>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BlockColliderMediator>(Lifetime.Singleton);
    }
}

