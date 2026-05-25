using VContainer;
using VContainer.Unity;

public class WindowMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PauseWindowLevelTrackerMediator>();
}
