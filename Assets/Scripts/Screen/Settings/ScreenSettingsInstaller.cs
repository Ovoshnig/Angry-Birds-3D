using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ScreenSettingsInstaller : IInstaller
{
    [SerializeField] private FullScreenToggleView _fullScreenToggleView;
    [SerializeField] private ResolutionDropdownView _resolutionDropdownView;
    [SerializeField] private VSyncToggleView _vSyncToggleView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_fullScreenToggleView);
        builder.RegisterInstance(_resolutionDropdownView);
        builder.RegisterInstance(_vSyncToggleView);

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
