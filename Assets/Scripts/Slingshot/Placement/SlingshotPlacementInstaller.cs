using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Slingshot.Placement
{
    [Serializable]
    public class SlingshotPlacementInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.Register<SlingshotBirdPlacer>(Lifetime.Singleton);
    }
}
