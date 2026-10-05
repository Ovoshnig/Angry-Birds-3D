using AngryBirds3D.Bird.Animation;
using AngryBirds3D.Bird.Destruction;
using AngryBirds3D.Bird.Entity;
using AngryBirds3D.Bird.ExplosionParticle;
using AngryBirds3D.Bird.FeatherParticle;
using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Bird.Input;
using AngryBirds3D.Bird.Points;
using AngryBirds3D.Bird.Power;
using AngryBirds3D.Bird.Queue;
using AngryBirds3D.Bird.Tracking;
using AngryBirds3D.Bird.TrailParticle;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Bird
{
    [Serializable]
    public class BirdInstaller : IInstaller
    {
        [SerializeField] private BirdEntityInstaller _entityInstaller;
        [SerializeField] private BirdQueueInstaller _queueInstaller;
        [SerializeField] private BirdFlightInstaller _flightInstaller;
        [SerializeField] private BirdDestructionInstaller _destructionInstaller;
        [SerializeField] private BirdTrackingInstaller _trackingInstaller;
        [SerializeField] private BirdPowerInstaller _powerInstaller;
        [SerializeField] private BirdPointsInstaller _pointsInstaller;
        [SerializeField] private BirdAnimationInstaller _animationInstaller;
        [SerializeField] private BirdTrailParticleInstaller _trailParticleInstaller;
        [SerializeField] private BirdFeatherParticleInstaller _featherParticleInstaller;
        [SerializeField] private BirdExplosionParticleInstaller _explosionParticleInstaller;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BirdInputProvider>().AsSelf();

            _entityInstaller.Install(builder);
            _queueInstaller.Install(builder);
            _flightInstaller.Install(builder);
            _destructionInstaller.Install(builder);
            _trackingInstaller.Install(builder);
            _powerInstaller.Install(builder);
            _pointsInstaller.Install(builder);
            _animationInstaller.Install(builder);
            _trailParticleInstaller.Install(builder);
            _featherParticleInstaller.Install(builder);
            _explosionParticleInstaller.Install(builder);
        }
    }
}
