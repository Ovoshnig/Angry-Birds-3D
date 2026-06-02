using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class RatingShowingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstancesInHierarchy<RatingShowerView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<RatingShower>().AsSelf();
            entryPoints.Add<RatingShowerViewsMediator>();
        });
    }
}
