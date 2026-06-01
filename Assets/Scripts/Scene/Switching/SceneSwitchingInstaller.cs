using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

[Serializable]
public class SceneSwitchingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<SceneSwitchButtonView> switchButtonViews = Object
            .FindObjectsByType<SceneSwitchButtonView>(FindObjectsInactive.Include);

        builder.RegisterInstance(switchButtonViews);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<SceneSwitchButtonViewsMediator>();
            entryPoints.Add<SaveStorageSceneButtonViewsMediator>();
        });
    }
}
