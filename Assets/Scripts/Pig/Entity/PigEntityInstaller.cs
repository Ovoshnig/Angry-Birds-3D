using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Pig.Entity
{
    [Serializable]
    public class PigEntityInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<PigEntityView>();
    }
}
