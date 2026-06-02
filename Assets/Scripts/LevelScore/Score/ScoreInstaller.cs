using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class ScoreInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<ScoreView>();
        builder.Register<ScoreModel>(Lifetime.Singleton);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<ScoreModelViewMediator>();
            entryPoints.Add<ScoreModelPointsPoolMediator>();
        });
    }
}
