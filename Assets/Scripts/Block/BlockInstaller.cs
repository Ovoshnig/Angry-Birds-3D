using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockInstaller : IInstaller
{
    [SerializeField] private BlockCollisionInstaller _blockCollisionInstaller;

    public void Install(IContainerBuilder builder) =>
        _blockCollisionInstaller.Install(builder);
}
