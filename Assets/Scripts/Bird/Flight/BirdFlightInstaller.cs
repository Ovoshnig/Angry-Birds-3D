using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdFlightInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.Register<BirdFlyer>(Lifetime.Singleton);
}

