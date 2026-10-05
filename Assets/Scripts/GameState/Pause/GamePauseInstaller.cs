using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.GameState.Pause
{
    [Serializable]
    public class GamePauseInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.Register<GamePauser>(Lifetime.Singleton);
    }
}
