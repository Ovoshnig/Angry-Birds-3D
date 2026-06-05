using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class ResolutionAdjustmentInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<ResolutionAdjustDropdownView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<ResolutionAdjuster>().AsSelf();
            entryPoints.Add<ResolutionAdjusterDropdownViewMediator>();
        });
    }
}
