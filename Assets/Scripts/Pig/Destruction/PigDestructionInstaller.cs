using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PigDestroyer>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PigDestroyerMediator>(Lifetime.Singleton);
    }
}
