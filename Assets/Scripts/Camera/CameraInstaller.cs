using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class CameraInstaller : IInstaller
{
    [SerializeField] private CameraSwitchingInstaller _switchingInstaller;

    public void Install(IContainerBuilder builder) =>
        _switchingInstaller.Install(builder);
}
