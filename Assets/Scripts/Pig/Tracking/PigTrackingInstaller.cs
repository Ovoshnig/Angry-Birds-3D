using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigTrackingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PigTracker>(Lifetime.Singleton).AsSelf();
}
