using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class SlingshotShootingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<SlingshotShooterView>();
        builder.RegisterEntryPoint<SlingshotShooter>().AsSelf();
    }
}
