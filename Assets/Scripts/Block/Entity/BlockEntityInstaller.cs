using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class BlockEntityInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<BlockEntityView>();
}
