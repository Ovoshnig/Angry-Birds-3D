using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockInstaller : IInstaller
{
    [SerializeField] private BlockEntityInstaller _blockEntityInstaller;
    [SerializeField] private BlockCollisionInstaller _blockCollisionInstaller;
    [SerializeField] private BlockDestructionInstaller _blockDestructionInstaller;

    public void Install(IContainerBuilder builder)
    {
        _blockEntityInstaller.Install(builder);
        _blockCollisionInstaller.Install(builder);
        _blockDestructionInstaller.Install(builder);
    }
}
