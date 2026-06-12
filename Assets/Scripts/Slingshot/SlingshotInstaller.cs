using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotInstaller : IInstaller
{
    [SerializeField] private PointerPositionInstaller _pointerPositionInstaller;
    [SerializeField] private SlingshotShootingInstaller _slingshotShootingInstaller;
    [SerializeField] private SlingshotPlacingInstaller _slingshotPlacingInstaller;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SlingshotInputProvider>().AsSelf();

        _pointerPositionInstaller.Install(builder);
        _slingshotShootingInstaller.Install(builder);
        _slingshotPlacingInstaller.Install(builder);
    }
}
