using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Entity
{
    [Serializable]
    public class BirdEntityInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<BirdEntityView>();
    }
}
