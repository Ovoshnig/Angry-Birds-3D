using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PointerPositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.Register<PointerPositionMeter>(Lifetime.Singleton);
}
