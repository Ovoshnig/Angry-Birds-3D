using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class WindowMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PauseWindowLevelTrackerMediator>();
            builder.RegisterEntryPoint<LevelTrackerPauseButtonViewMediator>();
        }
    }
}
