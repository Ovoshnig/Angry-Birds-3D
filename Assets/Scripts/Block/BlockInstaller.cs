using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockInstaller : IInstaller
{
    [SerializeField] private BlockCollisionInstaller _blockCollisionInstaller;
    [SerializeField] private BlockDestructionInstaller _blockDestructionInstaller;
    [SerializeField] private BlockSFXPlayingInstaller _blockSFXPlayingInstaller;

    public void Install(IContainerBuilder builder)
    {
        _blockCollisionInstaller.Install(builder);
        _blockDestructionInstaller.Install(builder);
        _blockSFXPlayingInstaller.Install(builder);
    }
}
