using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Animation
{
    [Serializable]
    public class BirdAnimationInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<WhiteBirdAnimatorViewPowerActivatorMediator>();
    }
}
