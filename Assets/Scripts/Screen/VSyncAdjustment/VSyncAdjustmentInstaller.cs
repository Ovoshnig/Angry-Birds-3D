using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Screen.VSyncAdjustment
{
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
}
