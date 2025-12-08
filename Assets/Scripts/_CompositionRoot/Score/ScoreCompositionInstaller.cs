using VContainer;
using VContainer.Unity;

public class ScoreCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ScoreModelBlockDestroyerMediator>(Lifetime.Singleton);
    }
}
