using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdTrackingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdTracker>().AsSelf();
}
