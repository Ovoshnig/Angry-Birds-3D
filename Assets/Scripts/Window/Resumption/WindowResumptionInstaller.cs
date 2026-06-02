using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class WindowResumptionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<ResumeButtonView>();
        builder.RegisterEntryPoint<WindowResumeButtonViewMediator>();
    }
}
