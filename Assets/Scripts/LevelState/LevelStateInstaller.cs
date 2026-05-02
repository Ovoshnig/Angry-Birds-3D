using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateInstaller : IInstaller
{
    [SerializeField] private LevelStateTrackingInstaller _stateTrackingInstaller;

    public void Install(IContainerBuilder builder) => _stateTrackingInstaller.Install(builder);
}
