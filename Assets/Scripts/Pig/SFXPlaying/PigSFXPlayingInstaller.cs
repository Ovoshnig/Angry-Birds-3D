using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigSFXPlayingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PigSFXPlayerMediator>(Lifetime.Singleton);
}
