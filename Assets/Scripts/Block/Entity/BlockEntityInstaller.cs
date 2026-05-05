using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockEntityInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        BlockEntityView[] blockEntityViews = _blockStructure.GetComponentsInChildren<BlockEntityView>();
        builder.RegisterInstance(blockEntityViews).As<DestructibleEntityView[]>().AsSelf();
    }
}
