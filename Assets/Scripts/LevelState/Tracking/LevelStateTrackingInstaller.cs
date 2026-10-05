using Ovoshnig.Extensions.VContainer;
using Ovoshnig.Scene.Switching;
using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.LevelState.Tracking
{
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
                entryPoints.Add<ActivityTracker>().AsSelf();
                entryPoints.Add<LevelStateTracker>().AsSelf();
                entryPoints.Add<ClearingPanelViewBirdPointsDisplayerMediator>();
                entryPoints.Add<SceneManagerLevelIndexViewMediator>();
                entryPoints.Add<ScoreModelFinalScoreViewMediator>();
                entryPoints.Add<FailurePanelViewLevelTrackerMediator>();
            });
        }
    }
}
