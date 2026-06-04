using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SFXCountInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.Register<SFXCounter>(Lifetime.Singleton);
}
