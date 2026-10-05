using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class ScoreMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<PointsPoolObjectDestroyerMediator>();
                entryPoints.Add<PointsPoolBirdDisplayerMediator>();
                entryPoints.Add<RatingEvaluatorBirdDisplayerMediator>();
                entryPoints.Add<ScoreViewCompletionPanelsMediator>();
            });
        }
    }
}
