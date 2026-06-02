using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class DataStorageResetInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstancesInHierarchy<DataResetButtonView>();
        builder.RegisterEntryPoint<DataStoragesResetButtonViewsMediator>();
    }
}
