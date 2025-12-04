using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockCollisionInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        builder.Register<BlockCollisionReporter>(Lifetime.Singleton);

        BlockDestructionView[] blockDestructionViews = _blockStructure
            .GetComponentsInChildren<BlockDestructionView>();
        builder.RegisterInstance(blockDestructionViews);

        builder.RegisterEntryPoint<BlockCollisionMediator>(Lifetime.Singleton);
    }
}

