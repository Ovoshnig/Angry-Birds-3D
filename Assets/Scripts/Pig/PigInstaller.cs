using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PigInstaller : IInstaller
{
    [SerializeField] private PigCollisionInstaller _pigCollisionInstaller;
    [SerializeField] private PigDestructionInstaller _pigDestructionInstaller;
    [SerializeField] private PigSFXPlayingInstaller _pigSFXPlayingInstaller;

    public void Install(IContainerBuilder builder)
    {
        _pigCollisionInstaller.Install(builder);
        _pigDestructionInstaller.Install(builder);
        _pigSFXPlayingInstaller.Install(builder);
    }
}
