using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockEntityInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<BlockEntityView> blockEntityViews = _blockStructure.GetComponentsInChildren<BlockEntityView>();
        builder.RegisterInstance(blockEntityViews);
    }
}
