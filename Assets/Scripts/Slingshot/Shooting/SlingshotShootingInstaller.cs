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

        builder.RegisterEntryPoint(resolver =>
        {
            SlingshotInputProvider slingshotInputProvider = resolver.Resolve<SlingshotInputProvider>();
            SlingshotSettings slingshotSettings = resolver.Resolve<SlingshotSettings>();

            return new SlingshotShooter(slingshotInputProvider, slingshotSettings,
                _slingshotShooterView.CenterAnchor,
                _slingshotShooterView.LeftRubber,
                _slingshotShooterView.RightRubber);
        }, Lifetime.Singleton).AsSelf();
    }
}
