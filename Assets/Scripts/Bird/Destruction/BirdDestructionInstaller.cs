using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Destruction
{
    [Serializable]
    public class BirdDestructionInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<BirdDestroyer>().AsSelf();
    }
}
