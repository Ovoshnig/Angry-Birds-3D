using Ovoshnig.Extensions.VContainer;
using Ovoshnig.Window.Window;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Window.Pause
{
    [Serializable]
    public class WindowPauseInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<PauseButtonView>();
            builder.RegisterEntryPoint<PauseMenuWindowButtonViewMediator>();
        }
    }
}
