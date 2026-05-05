using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigInstaller : IInstaller
{
    [SerializeField] private PigEntityInstaller _pigEntityInstaller;
    [SerializeField] private PigTrackingInstaller _pigTrackingInstaller;

    public void Install(IContainerBuilder builder)
    {
        _pigEntityInstaller.Install(builder);
        _pigTrackingInstaller.Install(builder);
    }
}
