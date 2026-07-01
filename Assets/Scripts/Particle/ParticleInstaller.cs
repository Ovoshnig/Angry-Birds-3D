using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class ParticleInstaller : IInstaller
{
    [SerializeField] private ParticleFeatherInstaller _featherInstaller;

    public void Install(IContainerBuilder builder)
    {
        _featherInstaller.Install(builder);
    }
}
