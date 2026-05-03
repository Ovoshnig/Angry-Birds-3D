using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class CameraSwitchingInstaller : IInstaller
{
    [SerializeField] private CameraSwitchView _cameraSwitchView;

    public void Install(IContainerBuilder builder) => builder.RegisterInstance(_cameraSwitchView);
}
