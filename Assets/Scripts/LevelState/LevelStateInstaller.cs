using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelStateInstaller : IInstaller
{
    [SerializeField] private LevelStateTrackingInstaller _stateTrackingInstaller;
    [SerializeField] private LevelStateAchievementInstaller _levelAchievementInstaller;
    [SerializeField] private LevelSFXSettings _sfxSettings;

    public void Install(IContainerBuilder builder)
    {
        _stateTrackingInstaller.Install(builder);
        _levelAchievementInstaller.Install(builder);

        builder.RegisterInstance(_sfxSettings);
    }
}
