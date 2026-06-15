using VContainer;
using VContainer.Unity;

public class ObjectCollisionMediatorsInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<ObjectColliderStartCameraSwitchMediator>();
}
