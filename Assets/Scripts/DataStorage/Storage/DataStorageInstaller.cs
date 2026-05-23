using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class DataStorageInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SaveStorage>().AsSelf();
            entryPoints.Add<SettingsStorage>().AsSelf();
        });
    }
}
