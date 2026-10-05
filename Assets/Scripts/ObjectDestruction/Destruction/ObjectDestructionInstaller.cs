using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.ObjectDestruction.Destruction
{
    [Serializable]
    public class ObjectDestructionInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<ObjectDestroyer>().AsSelf();
    }
}
