using System;
using VContainer;
using VContainer.Unity;
using VContainer.Extensions;

[Serializable]
public class GameQuittingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstanceInHierarchy<GameQuitButtonView>();
        builder.Register<GameQuitter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameQuitterButtonViewMediator>();
    }
}
