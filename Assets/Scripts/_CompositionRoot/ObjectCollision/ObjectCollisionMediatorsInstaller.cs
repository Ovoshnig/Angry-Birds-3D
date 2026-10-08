using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class ObjectCollisionMediatorsInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<ObjectColliderStartCameraSwitchMediator>();
    }
}
