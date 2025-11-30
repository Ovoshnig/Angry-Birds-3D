using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdInstaller : IInstaller
{
    [SerializeField] private BirdQueueInstaller _birdQueueInstaller;

    public void Install(IContainerBuilder builder)
    {
        _birdQueueInstaller.Install(builder);
    }
}
