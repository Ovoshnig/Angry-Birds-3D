using Ovoshnig.Skybox.Rotation;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Skybox
{
    [Serializable]
    public class SkyboxInstaller : IInstaller
    {
        [SerializeField] private SkyboxRotationInstaller _rotationInstaller;

        public void Install(IContainerBuilder builder) => _rotationInstaller.Install(builder);
    }
}
