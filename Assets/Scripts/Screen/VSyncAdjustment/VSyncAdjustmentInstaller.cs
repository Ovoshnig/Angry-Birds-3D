using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class VSyncAdjustmentInstaller : IInstaller
{
    [SerializeField] private VSyncAdjusterView _adjusterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_adjusterView);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<VSyncAdjuster>().AsSelf();
            entryPoints.Add<VSyncAdjusterViewMediator>();
        });
    }
}
