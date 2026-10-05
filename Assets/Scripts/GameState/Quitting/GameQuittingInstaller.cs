using Ovoshnig.Extensions.VContainer;
using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.GameState.Quitting
{
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
}
