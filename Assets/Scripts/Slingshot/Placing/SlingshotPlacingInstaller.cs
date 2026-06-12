using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotPlacingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.Register<SlingshotBirdPlacer>(Lifetime.Singleton);
}
