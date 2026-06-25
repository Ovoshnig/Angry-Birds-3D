using System;
using UnityEngine;
using VContainer;
using VContainer.Extensions;
using VContainer.Unity;

[Serializable]
public class BirdFlightInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<Terrain>();
        builder.Register<BirdFlyer>(Lifetime.Singleton);
    }
}
