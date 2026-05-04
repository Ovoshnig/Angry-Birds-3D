using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SceneInstaller : IInstaller
{
    [SerializeField] private RectTransform _sceneViewsParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<SceneButtonView> sceneViews = _sceneViewsParent.GetComponentsInChildren<SceneButtonView>(true);
        builder.RegisterInstance(sceneViews);

        builder.RegisterEntryPoint<SceneSwitch>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<SceneSwitchMediator>(Lifetime.Singleton);
    }
}
