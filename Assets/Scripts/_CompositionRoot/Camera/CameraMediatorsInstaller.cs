using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class CameraMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<CameraSwitchViewBirdFlyerMediator>();
                entryPoints.Add<CameraSwitchViewEggDroppingBirdPowerMediator>();
                entryPoints.Add<CameraSwitchViewLevelTrackerMediator>();
            });
        }
    }
}
