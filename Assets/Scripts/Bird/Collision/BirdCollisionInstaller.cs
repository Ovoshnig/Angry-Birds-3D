using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<BirdCollider>(Lifetime.Singleton);
        builder.RegisterEntryPoint<BirdColliderMediator>(Lifetime.Singleton);
    }
}
