using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockParticleInstaller : IInstaller
{
    [SerializeField] private BlockParticleView _blockParticlePrefab;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponentInNewPrefab(_blockParticlePrefab, Lifetime.Singleton);
        builder.RegisterEntryPoint<BlockParticleViewObjectDestroyerMediator>();
    }
}
