using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Tracking
{
    [Serializable]
    public class BirdTrackingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<BirdTracker>().AsSelf();
    }
}
