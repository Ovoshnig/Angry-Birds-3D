using System;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Entity
{
    [Serializable]
    public class BirdEntityInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.RegisterInstancesInHierarchy<BirdEntityView>();
    }
}
