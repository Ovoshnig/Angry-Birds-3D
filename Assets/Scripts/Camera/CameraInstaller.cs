using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class CameraInstaller : IInstaller
{
    [SerializeField] private CameraNoiseView _cameraNoiseView;

    public void Install(IContainerBuilder builder) =>
        builder.RegisterInstance(_cameraNoiseView);
}
