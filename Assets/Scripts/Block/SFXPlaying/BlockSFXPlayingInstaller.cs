using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockSFXPlayingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<BlockSFXPlayerMediator>(Lifetime.Singleton);
}
