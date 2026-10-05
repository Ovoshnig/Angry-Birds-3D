using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.DataStorage.Reset
{
    [Serializable]
    public class DataStorageResetInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstancesInHierarchy<DataResetButtonView>();
            builder.RegisterEntryPoint<DataStoragesResetButtonViewsMediator>();
        }
    }
}
