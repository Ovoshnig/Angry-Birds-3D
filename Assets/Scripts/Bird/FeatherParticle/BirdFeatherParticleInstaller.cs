using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird.FeatherParticle
{
    [Serializable]
    public class BirdFeatherParticleInstaller : IInstaller
    {
        [SerializeField] private FeatherParticleView _featherParticlePrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_featherParticlePrefab, Lifetime.Singleton);

            builder.UseEntryPoints(entryPoints =>
            {
                entryPoints.Add<FeatherParticleViewObjectColliderMediator>();
                entryPoints.Add<FeatherParticleViewBirdDestroyerMediator>();
            });
        }
    }
}
