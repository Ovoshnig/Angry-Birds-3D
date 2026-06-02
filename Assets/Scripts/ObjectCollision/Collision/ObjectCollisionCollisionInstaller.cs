using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ObjectCollisionCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<CollisionEvaluator>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<ObjectCollider>().AsSelf();
    }
}
