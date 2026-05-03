using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PigCollider>(Lifetime.Singleton).AsSelf();
}
