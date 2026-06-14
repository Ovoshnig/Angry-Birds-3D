using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SkyboxRotationInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.RegisterEntryPoint<SkyboxRotator>().AsSelf();
}
