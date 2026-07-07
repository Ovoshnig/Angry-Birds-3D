using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

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
