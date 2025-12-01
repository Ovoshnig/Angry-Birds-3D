using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotInstaller : IInstaller
{
    [SerializeField] private SlingshotShooterView _slingshotShooterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SlingshotInputHandler>(Lifetime.Singleton)
            .AsSelf();

        builder.RegisterComponent(_slingshotShooterView);
    }
}
