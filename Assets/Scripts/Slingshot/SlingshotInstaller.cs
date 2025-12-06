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

        builder.RegisterInstance(_slingshotShooterView);

        builder.RegisterEntryPoint(resolver =>
        {
            SlingshotInputHandler slingshotInputHandler = resolver.Resolve<SlingshotInputHandler>();
            SlingshotSettings slingshotSettings = resolver.Resolve<SlingshotSettings>();

            return new SlingshotShooter(slingshotInputHandler, slingshotSettings,
                _slingshotShooterView.CenterAnchor,
                _slingshotShooterView.LeftAnchor.position,
                _slingshotShooterView.RightAnchor.position,
                _slingshotShooterView.CenterAnchor.position,
                _slingshotShooterView.LeftRubber,
                _slingshotShooterView.RightRubber);
        }, Lifetime.Singleton).AsSelf();
    }
}
