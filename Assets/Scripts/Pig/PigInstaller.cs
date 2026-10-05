using AngryBirds3D.Pig.Entity;
using AngryBirds3D.Pig.Tracking;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Pig
{
    [Serializable]
    public class PigInstaller : IInstaller
    {
        [SerializeField] private PigEntityInstaller _entityInstaller;
        [SerializeField] private PigTrackingInstaller _trackingInstaller;

        public void Install(IContainerBuilder builder)
        {
            _entityInstaller.Install(builder);
            _trackingInstaller.Install(builder);
        }
    }
}
