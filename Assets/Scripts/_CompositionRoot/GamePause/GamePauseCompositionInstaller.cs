using VContainer;
using VContainer.Unity;

public class GamePauseCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => 
        builder.RegisterEntryPoint<GamePauserGameOverMediator>(Lifetime.Singleton);
}
