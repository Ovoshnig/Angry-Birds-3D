using UnityEngine;
using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GameQuittingInstaller : IInstaller
{
    [SerializeField] private GameQuitterButtonView _quitButtonView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_quitButtonView);
        builder.Register<GameQuitter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameQuitterMediator>(Lifetime.Singleton);
    }
}
