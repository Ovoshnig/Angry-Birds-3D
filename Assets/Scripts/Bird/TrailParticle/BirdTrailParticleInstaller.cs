using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class BirdTrailParticleInstaller : IInstaller
{
    [SerializeField] private TrailParticleView _trailParticlePrefab;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_trailParticlePrefab);
        builder.RegisterEntryPoint<TrailParticlePlayer>(Lifetime.Singleton).AsSelf();
    }
}
