using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GamePauseInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.Register<GamePauser>(Lifetime.Singleton);
}
