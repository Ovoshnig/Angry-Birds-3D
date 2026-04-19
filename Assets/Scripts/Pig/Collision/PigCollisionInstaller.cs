using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<PigCollider>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PigColliderMediator>(Lifetime.Singleton);
    }
}
