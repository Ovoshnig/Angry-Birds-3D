using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigInstaller : IInstaller
{
    [SerializeField] private PigEntityInstaller _pigEntityInstaller;
    [SerializeField] private PigCollisionInstaller _pigCollisionInstaller;
    [SerializeField] private PigDestructionInstaller _pigDestructionInstaller;
    [SerializeField] private PigTrackingInstaller _pigTrackingInstaller;

    public void Install(IContainerBuilder builder)
    {
        _pigEntityInstaller.Install(builder);
        _pigCollisionInstaller.Install(builder);
        _pigDestructionInstaller.Install(builder);
        _pigTrackingInstaller.Install(builder);
    }
}
