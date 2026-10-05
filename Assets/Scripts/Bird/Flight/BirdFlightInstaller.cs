using Ovoshnig.Extensions.VContainer;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.Flight
{
    [Serializable]
    public class BirdFlightInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstanceInHierarchy<Terrain>();
            builder.Register<BirdFlyer>(Lifetime.Singleton);
        }
    }
}
