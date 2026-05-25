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
        IReadOnlyList<SceneButtonView> sceneViews = Object
            .FindObjectsByType<SceneButtonView>(FindObjectsInactive.Include);

        builder.RegisterInstance(sceneViews);
        builder.RegisterEntryPoint<SceneSwitchViewsMediator>();
    }
}
