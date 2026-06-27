using VContainer;
using VContainer.Unity;

public class CursorMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<CursorShowerWindowTrackerMediator>();
}
