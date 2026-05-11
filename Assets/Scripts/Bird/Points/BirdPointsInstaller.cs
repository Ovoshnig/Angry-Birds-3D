using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdPointsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.Register<BirdPointsDisplayer>(Lifetime.Singleton);
}
