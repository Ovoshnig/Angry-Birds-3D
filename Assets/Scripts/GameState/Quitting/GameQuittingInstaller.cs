using UnityEngine;
using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GameQuittingInstaller : IInstaller
{
    [SerializeField] private GameQuitterButtonView _quitterButtonView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_quitterButtonView);
        builder.Register<GameQuitter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameQuitterMediator>();
    }
}
