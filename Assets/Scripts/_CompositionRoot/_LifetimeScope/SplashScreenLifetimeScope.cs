using VContainer;
using VContainer.Unity;

public class SplashScreenLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SplashScreenDisplayer>().AsSelf();
        builder.RegisterEntryPoint<SceneSwitchSplashScreenDisplayerMediator>();
    }
}
