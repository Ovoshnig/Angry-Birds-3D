using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
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
