using AngryBirds3D.Slingshot.Input;
using AngryBirds3D.Slingshot.Placement;
using AngryBirds3D.Slingshot.PointerPosition;
using AngryBirds3D.Slingshot.Shooting;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Slingshot
{
    [Serializable]
    public class SlingshotInstaller : IInstaller
    {
        [SerializeField] private PointerPositionInstaller _pointerPositionInstaller;
        [SerializeField] private SlingshotShootingInstaller _slingshotShootingInstaller;
        [SerializeField] private SlingshotPlacementInstaller _slingshotPlacementInstaller;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SlingshotInputProvider>().AsSelf();

            _pointerPositionInstaller.Install(builder);
            _slingshotShootingInstaller.Install(builder);
            _slingshotPlacementInstaller.Install(builder);
        }
    }
}
