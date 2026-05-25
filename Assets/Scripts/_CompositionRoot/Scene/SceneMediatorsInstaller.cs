using VContainer;
using VContainer.Unity;

public class SceneMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<SceneSwitchSplashScreenDisplayerMediator>();
}
