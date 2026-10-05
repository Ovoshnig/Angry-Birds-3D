using Ovoshnig.Extensions.VContainer;
using Ovoshnig.Window.Window;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Window.Resumption
{
    [Serializable]
    public class WindowResumptionInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<ResumeButtonView>();
            builder.RegisterEntryPoint<WindowResumeButtonViewMediator>();
        }
    }
}
