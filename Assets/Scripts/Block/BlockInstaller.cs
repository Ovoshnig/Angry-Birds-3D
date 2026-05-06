using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockInstaller : IInstaller
{
    [SerializeField] private BlockEntityInstaller _blockEntityInstaller;

    public void Install(IContainerBuilder builder) => _blockEntityInstaller.Install(builder);
}
