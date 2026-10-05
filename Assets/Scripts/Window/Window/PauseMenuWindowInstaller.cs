using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Window.Window
{
    [Serializable]
    public class PauseMenuWindowInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<WindowView>();

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<PauseMenuWindow>().AsSelf().As<Window>();
                entryPoints.Add<WindowMediator>();
            });
        }
    }
}
