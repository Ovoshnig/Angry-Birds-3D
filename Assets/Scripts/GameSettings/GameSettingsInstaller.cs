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
        builder.RegisterInstance(_gameSettings.AudioSettings);
        builder.RegisterInstance(_gameSettings.SlingshotSettings);
        builder.RegisterInstance(_gameSettings.BirdSettings);
        builder.RegisterInstance(_gameSettings.BlockSettings);
    }
}
