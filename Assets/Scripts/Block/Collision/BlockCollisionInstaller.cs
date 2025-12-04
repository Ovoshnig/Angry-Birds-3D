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

        BlockDestroyerView[] blockDestroyerViews = _blockStructure
            .GetComponentsInChildren<BlockDestroyerView>();
        builder.RegisterInstance(blockDestroyerViews);

        builder.RegisterEntryPoint<BlockCollisionMediator>(Lifetime.Singleton);
    }
}

