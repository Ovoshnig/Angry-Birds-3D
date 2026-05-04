using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class SceneSettingsInstaller : IInstaller
{
    [SerializeField] private RectTransform _sceneViewsParent;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_sceneViewsParent.GetComponentInChildren<FullScreenToggleView>(true));
        builder.RegisterInstance(_sceneViewsParent.GetComponentInChildren<ResolutionDropdownView>(true));
        builder.RegisterInstance(_sceneViewsParent.GetComponentInChildren<VSyncToggleView>(true));

        builder.RegisterEntryPoint<FullScreenAdjuster>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<ResolutionAdjuster>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<VSyncAdjuster>(Lifetime.Singleton).AsSelf();

        builder.RegisterEntryPoint<FullScreenAdjusterMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<ResolutionAdjusterMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<VSyncAdjusterMediator>(Lifetime.Singleton);
    }
}
