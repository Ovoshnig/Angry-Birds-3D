using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Queue
{
    [Serializable]
    public class BirdQueueInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<BirdQueue>().AsSelf();
    }
}
