using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class SceneSwitchingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstancesInHierarchy<SceneSwitchButtonView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SceneSwitchButtonViewsMediator>();
            entryPoints.Add<SaveStorageSceneButtonViewsMediator>();
        });
    }
}
