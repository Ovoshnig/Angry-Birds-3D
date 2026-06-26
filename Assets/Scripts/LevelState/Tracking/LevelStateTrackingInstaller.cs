using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<ClearingPanelView>();
        builder.RegisterInstanceInHierarchy<FinalScoreView>();
        builder.RegisterInstanceInHierarchy<FailurePanelView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<LevelStateTracker>().AsSelf();
            entryPoints.Add<ScoreModelFinalScoreViewMediator>();
            entryPoints.Add<ClearingPanelViewBirdPointsDisplayerMediator>();
            entryPoints.Add<FailurePanelViewLevelTrackerMediator>();
        });
    }
}
