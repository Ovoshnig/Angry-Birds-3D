using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

namespace AngryBirds3D.Block.Entity
{
    [Serializable]
    public class BlockEntityInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<BlockEntityView>();
    }
}
