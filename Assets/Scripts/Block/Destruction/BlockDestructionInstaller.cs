using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BlockDestroyer>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<BlockDestroyerMediator>(Lifetime.Singleton);
    }
}
