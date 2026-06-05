using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class PanelSwitchingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstancesInHierarchy<PanelCloseButtonView>();
        builder.RegisterEntryPoint<InputProviderCloseButtonViewsMediator>();
    }
}
