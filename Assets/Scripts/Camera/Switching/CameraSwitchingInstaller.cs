using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class CameraSwitchingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<CameraSwitchView>();
        builder.RegisterEntryPoint<StartCameraSwitch>().AsSelf();
    }
}
