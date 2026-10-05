using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.ObjectCollision.Collision
{
    [Serializable]
    public class ObjectCollisionCollisionInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CollisionEvaluator>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ObjectCollider>().AsSelf();
        }
    }
}
