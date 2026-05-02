using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.Register<LevelStateTracker>(Lifetime.Singleton);
}
