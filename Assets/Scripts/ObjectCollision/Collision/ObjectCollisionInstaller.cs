using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ObjectCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<ObjectCollider>(Lifetime.Singleton).AsSelf();
}
