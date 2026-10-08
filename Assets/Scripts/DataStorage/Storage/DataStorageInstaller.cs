using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.DataStorage.Storage
{
    [Serializable]
    public class DataStorageInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<SaveStorage>().As<DataStorage>().AsSelf();
                entryPoints.Add<SettingsStorage>().As<DataStorage>().AsSelf();
            });
        }
    }
}
