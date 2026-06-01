using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class VSyncAdjustmentInstaller : IInstaller
{
    [SerializeField] private VSyncAdjustToggleView _toggleView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_toggleView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<VSyncAdjuster>().AsSelf();
            entryPoints.Add<VSyncAdjusterToggleViewMediator>();
        });
    }
}
