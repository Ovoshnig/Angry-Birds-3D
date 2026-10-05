using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.UI.PanelSwitching
{
    [Serializable]
    public class PanelSwitchingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstancesInHierarchy<PanelCloseButtonView>();
            builder.RegisterEntryPoint<InputProviderCloseButtonViewsMediator>();
        }
    }
}
