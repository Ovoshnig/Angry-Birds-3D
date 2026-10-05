using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class SceneMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<SceneSwitchSplashScreenDisplayerMediator>();
    }
}
