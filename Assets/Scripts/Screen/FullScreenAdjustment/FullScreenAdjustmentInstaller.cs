using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Screen.FullScreenAdjustment
{
    [Serializable]
    public class FullScreenAdjustmentInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<FullScreenAdjustToggleView>();
            builder.RegisterEntryPoint<FullScreenAdjusterToggleViewMediator>();
        }
    }
}
