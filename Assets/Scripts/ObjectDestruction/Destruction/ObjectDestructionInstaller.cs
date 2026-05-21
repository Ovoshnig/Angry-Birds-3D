using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ObjectDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<ObjectDestroyer>().AsSelf();
}
