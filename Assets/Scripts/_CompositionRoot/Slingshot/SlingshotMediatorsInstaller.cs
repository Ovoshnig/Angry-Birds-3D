using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class SlingshotMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<SlingshotShooterGamePauserMediator>();
                entryPoints.Add<SlingshotShooterLevelTrackerMediator>();
                entryPoints.Add<SlingshotBirdPlacerLevelTrackerMediator>();
            });
        }
    }
}
