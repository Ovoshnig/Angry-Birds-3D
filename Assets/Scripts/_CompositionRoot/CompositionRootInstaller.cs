using VContainer;
using VContainer.Unity;

public class CompositionRootInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        new ScoreCompositionInstaller().Install(builder);
        new CameraCompositionInstaller().Install(builder);
        new SFXCompositionInstaller().Install(builder);
        new BirdCompositionInstaller().Install(builder);
        new SlingshotCompositionInstaller().Install(builder);
    }
}
