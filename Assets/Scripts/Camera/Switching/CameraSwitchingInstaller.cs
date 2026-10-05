using AngryBirds3D.Camera.Switching;
using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Camera.Switching
{
    [Serializable]
    public class CameraSwitchingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<CameraSwitchView>();
            builder.RegisterEntryPoint<StartCameraSwitch>().AsSelf();
        }
    }
}
