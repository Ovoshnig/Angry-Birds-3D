using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Screen.ResolutionAdjustment
{
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
}
