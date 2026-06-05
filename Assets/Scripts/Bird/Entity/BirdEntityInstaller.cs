using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class BirdEntityInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<BirdEntityView>();
}
