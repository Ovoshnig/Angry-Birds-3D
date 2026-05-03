using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotInstaller : IInstaller
{
    [SerializeField] private SlingshotShootingInstaller _slingshotShootingInstaller;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SlingshotInputHandler>(Lifetime.Singleton)
            .AsSelf();

        _slingshotShootingInstaller.Install(builder);
    }
}
