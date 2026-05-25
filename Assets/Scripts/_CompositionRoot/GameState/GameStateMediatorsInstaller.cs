using VContainer;
using VContainer.Unity;

public class GameStateMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<GamePauserPauseMenuWindowMediator>();
}
