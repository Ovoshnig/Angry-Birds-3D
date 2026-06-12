using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotPlacementInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.Register<SlingshotBirdPlacer>(Lifetime.Singleton);
}
