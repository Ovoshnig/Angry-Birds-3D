using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class VSyncAdjustmentInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<VSyncAdjustToggleView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<VSyncAdjuster>().AsSelf();
            entryPoints.Add<VSyncAdjusterToggleViewMediator>();
        });
    }
}
