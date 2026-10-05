using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

namespace AngryBirds3D.Slingshot.Shooting
{
    [Serializable]
    public class SlingshotShootingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<SlingshotShooterView>();
            builder.RegisterEntryPoint<SlingshotShooter>().AsSelf();
        }
    }
}
