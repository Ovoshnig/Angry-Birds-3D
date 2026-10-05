using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.ObjectCollision.Entity
{
    [Serializable]
    public class ObjectCollisionEntityInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<CollidableEntityView>();
    }
}
