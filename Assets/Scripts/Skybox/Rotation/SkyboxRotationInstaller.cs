using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Skybox.Rotation
{
    [Serializable]
    public class SkyboxRotationInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterEntryPoint<SkyboxRotator>().AsSelf();
    }
}
