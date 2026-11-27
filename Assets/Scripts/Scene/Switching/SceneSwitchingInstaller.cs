using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Scene.Switching;

[Serializable]
public class SceneSwitchingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstancesInHierarchy<SceneSwitchPanelView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SceneSwitchPanelsMediator>();
            entryPoints.Add<SaveStorageSceneSwitchPanelsMediator>();
        });
    }
}
