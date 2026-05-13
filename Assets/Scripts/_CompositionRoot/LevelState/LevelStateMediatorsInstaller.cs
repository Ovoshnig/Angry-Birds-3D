using VContainer;
using VContainer.Unity;

public class LevelStateMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<ClearingPanelBirdPointsDisplayerMediator>(Lifetime.Singleton);
}
