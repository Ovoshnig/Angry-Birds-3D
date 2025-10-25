using VContainer;
using VContainer.Unity;

public class CompositionRootInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => new GamePauseCompositionInstaller().Install(builder);
}
