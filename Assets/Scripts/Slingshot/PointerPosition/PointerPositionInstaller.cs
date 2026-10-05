using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Slingshot.PointerPosition
{
    [Serializable]
    public class PointerPositionInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.Register<PointerPositionMeter>(Lifetime.Singleton);
    }
}
