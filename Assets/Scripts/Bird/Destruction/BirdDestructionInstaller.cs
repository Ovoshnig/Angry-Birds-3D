using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BirdDestroyer>(Lifetime.Singleton).AsSelf();
}
