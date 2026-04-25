using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdInstaller : IInstaller
{
    [SerializeField] private BirdEntityInstaller _entityInstaller;
    [SerializeField] private BirdQueueInstaller _queueInstaller;
    [SerializeField] private BirdFlightInstaller _flightInstaller;
    [SerializeField] private BirdCollisionInstaller _collisionInstaller;

    public void Install(IContainerBuilder builder)
    {
        _entityInstaller.Install(builder);
        _queueInstaller.Install(builder);
        _flightInstaller.Install(builder);
        _collisionInstaller.Install(builder);
    }
}
