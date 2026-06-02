using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class FullScreenAdjustmentInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<FullScreenAdjustToggleView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<FullScreenAdjuster>().AsSelf();
            entryPoints.Add<FullScreenAdjusterToggleViewMediator>();
        });
    }
}
