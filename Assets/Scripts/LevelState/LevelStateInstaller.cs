using AngryBirds3D.LevelState.Achievement;
using AngryBirds3D.LevelState.Tracking;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.LevelState
{
    [Serializable]
    public class LevelStateInstaller : IInstaller
    {
        [SerializeField] private LevelStateTrackingInstaller _stateTrackingInstaller;
        [SerializeField] private LevelStateAchievementInstaller _levelAchievementInstaller;
        [SerializeField] private LevelSfxProfile _sfxProfile;

        public void Install(IContainerBuilder builder)
        {
            _stateTrackingInstaller.Install(builder);
            _levelAchievementInstaller.Install(builder);

            builder.RegisterInstance(_sfxProfile);
        }
    }
}
