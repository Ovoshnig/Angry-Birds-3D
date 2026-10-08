using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class GameStateMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<GamePauserPauseMenuWindowMediator>();
    }
}
