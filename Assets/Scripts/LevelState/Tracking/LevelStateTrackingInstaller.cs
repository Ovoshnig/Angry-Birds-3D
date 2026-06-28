using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class LevelStateTrackingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<ClearingPanelView>().AsSelf().As<CompletionPanelView>();
        builder.RegisterInstanceInHierarchy<LevelIndexView>();
        builder.RegisterInstanceInHierarchy<FinalScoreView>();
        builder.RegisterInstanceInHierarchy<FailurePanelView>().AsSelf().As<CompletionPanelView>();

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<LevelStateTracker>().AsSelf();
            entryPoints.Add<ClearingPanelViewBirdPointsDisplayerMediator>();
            entryPoints.Add<SceneManagerLevelIndexViewMediator>();
            entryPoints.Add<ScoreModelFinalScoreViewMediator>();
            entryPoints.Add<FailurePanelViewLevelTrackerMediator>();
        });
    }
}
