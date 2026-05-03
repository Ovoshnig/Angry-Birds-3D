using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdCollider>(Lifetime.Singleton).AsSelf();
}
