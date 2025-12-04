using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockDestructionInstaller : IInstaller
{
    [SerializeField] private GameObject _blockStructure;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BlockDestroyer>(Lifetime.Singleton).AsSelf();

        BlockDestroyerView[] blockDestroyerViews = _blockStructure
            .GetComponentsInChildren<BlockDestroyerView>();
        builder.RegisterInstance(blockDestroyerViews);

        builder.RegisterEntryPoint<BlockDestroyerMediator>(Lifetime.Singleton);
    }
}
