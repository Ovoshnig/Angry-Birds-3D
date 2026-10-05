using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.GameState.SocialLink
{
    [Serializable]
    public class SocialLinkInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstancesInHierarchy<SocialLinkButtonView>();
            builder.Register<SocialLinkOpener>(Lifetime.Singleton);
            builder.RegisterEntryPoint<SocialLinkOpenerButtonViewsMediator>();
        }
    }
}
