using AngryBirds3D.Camera.Switching;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Camera
{
    [Serializable]
    public class CameraInstaller : IInstaller
    {
        [SerializeField] private CameraSwitchingInstaller _switchingInstaller;

        public void Install(IContainerBuilder builder) =>
            _switchingInstaller.Install(builder);
    }
}
