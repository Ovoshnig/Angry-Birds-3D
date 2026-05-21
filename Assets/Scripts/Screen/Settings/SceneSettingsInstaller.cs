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

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<FullScreenAdjuster>().AsSelf();
            entryPoints.Add<ResolutionAdjuster>().AsSelf();
            entryPoints.Add<VSyncAdjuster>().AsSelf();

            entryPoints.Add<FullScreenAdjusterMediator>();
            entryPoints.Add<ResolutionAdjusterMediator>();
            entryPoints.Add<VSyncAdjusterMediator>();
        });
    }
}
