using Ovoshnig.ObjectCollision.Collision;
using Ovoshnig.ObjectCollision.Entity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.ObjectCollision
{
    [Serializable]
    public class ObjectCollisionInstaller : IInstaller
    {
        [SerializeField] private ObjectCollisionCollisionInstaller _collisionInstaller;
        [SerializeField] private ObjectCollisionEntityInstaller _entityInstaller;
        public void Install(IContainerBuilder builder)
        {
            _collisionInstaller.Install(builder);
            _entityInstaller.Install(builder);
        }
    }
}
