using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Scene.Switching
{
    [Serializable]
    public class SceneSwitchingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstancesInHierarchy<SceneSwitchButtonView>();

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<SceneSwitchButtonViewsMediator>();
                entryPoints.Add<SaveStorageSceneButtonViewsMediator>();
            });
        }
    }
}
