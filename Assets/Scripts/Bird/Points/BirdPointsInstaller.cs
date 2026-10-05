using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Points
{
    [Serializable]
    public class BirdPointsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.Register<BirdPointsDisplayer>(Lifetime.Singleton);
    }
}
