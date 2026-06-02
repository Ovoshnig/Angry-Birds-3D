using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class WindowPauseInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<PauseButtonView>();
        builder.RegisterEntryPoint<PauseMenuWindowButtonViewMediator>();
    }
}
