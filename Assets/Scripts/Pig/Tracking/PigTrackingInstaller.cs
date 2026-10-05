using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Pig.Tracking
{
    [Serializable]
    public class PigTrackingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<PigTracker>().AsSelf();
    }
}
