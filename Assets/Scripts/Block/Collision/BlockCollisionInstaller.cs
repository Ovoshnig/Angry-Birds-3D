using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BlockCollider>(Lifetime.Singleton).AsSelf();
}
