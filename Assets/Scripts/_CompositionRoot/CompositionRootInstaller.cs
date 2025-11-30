using VContainer;
using VContainer.Unity;

public class CompositionRootInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        new LevelStateCompositionInstaller().Install(builder);
        new BirdCompositionInstaller().Install(builder);
        new SlingshotCompositionInstaller().Install(builder);
    }
}
