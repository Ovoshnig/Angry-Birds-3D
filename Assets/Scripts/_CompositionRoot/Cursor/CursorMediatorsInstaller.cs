using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class CursorMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<CursorStateModelSlingshotShooterMediator>();
                entryPoints.Add<CursorStateModelWindowTrackerMediator>();
                entryPoints.Add<CursorStateModelCompletionPanelViewsMediator>();
            });
        }
    }
}
