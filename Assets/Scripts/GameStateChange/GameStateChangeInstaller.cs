using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GameStateChangeInstaller : IInstaller
{

    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameRestarterView _gameRestarterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameStateChanger>(Lifetime.Singleton)
            .AsSelf();

        builder.RegisterComponent(_gameOverView);
        builder.RegisterEntryPoint<GameOver>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<GameOverMediator>(Lifetime.Singleton);

        builder.RegisterComponent(_gameRestarterView);
        builder.Register<GameRestarter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameRestarterMediator>(Lifetime.Singleton);
    }
}
