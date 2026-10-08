using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.ExplosionParticle
{
    [Serializable]
    public class BirdExplosionParticleInstaller : IInstaller
    {
        [SerializeField] private ExplosionParticleView _explosionParticlePrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_explosionParticlePrefab, Lifetime.Singleton);
            builder.RegisterEntryPoint<ExplosionParticleViewBirdExploderMediator>();
        }
    }
}
