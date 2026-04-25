using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdQueueInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdQueue>(Lifetime.Singleton).AsSelf();
}
