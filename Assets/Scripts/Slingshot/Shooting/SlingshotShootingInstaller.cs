using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SlingshotShootingInstaller : IInstaller
{
    [SerializeField] private SlingshotShooterView _slingshotShooterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_slingshotShooterView);
        builder.RegisterEntryPoint<SlingshotShooter>(Lifetime.Singleton).AsSelf();
    }
}
