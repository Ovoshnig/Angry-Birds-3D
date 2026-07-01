using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BlockInstaller : IInstaller
{
    [SerializeField] private BlockEntityInstaller _entityInstaller;
    [SerializeField] private BlockParticleInstaller _particleInstaller;

    public void Install(IContainerBuilder builder)
    {
        _entityInstaller.Install(builder);
        _particleInstaller.Install(builder);
    }
}
