using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GameSettingsInstaller : IInstaller
{
    [SerializeField] private GameSettings _gameSettings;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameSettings.SceneSettings);
        builder.RegisterInstance(_gameSettings.CameraSettings);
        builder.RegisterInstance(_gameSettings.AudioSettings);
        builder.RegisterInstance(_gameSettings.TrailParticleSettings);
        builder.RegisterInstance(_gameSettings.SkyboxSettings);
        builder.RegisterInstance(_gameSettings.CollisionSettings);
        builder.RegisterInstance(_gameSettings.SlingshotSettings);
        builder.RegisterInstance(_gameSettings.SlingshotPlacingSettings);
        builder.RegisterInstance(_gameSettings.BirdSettings);
        builder.RegisterInstance(_gameSettings.BirdStretchSettings);
        builder.RegisterInstance(_gameSettings.BirdPowerSettings.SplitInto3PowerSettings);
        builder.RegisterInstance(_gameSettings.BirdPowerSettings.BoostPowerSettings);
        builder.RegisterInstance(_gameSettings.BirdPowerSettings.ExplosionPowerSettings);
        builder.RegisterInstance(_gameSettings.ScoreSettings);
    }
}
