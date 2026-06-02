using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class PigEntityInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<PigEntityView>();
}
