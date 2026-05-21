using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ObjectCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<CollisionEvaluator>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<ObjectCollider>().AsSelf();
    }
}
