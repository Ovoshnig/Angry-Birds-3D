using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class CursorShowingInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) => builder.Register<CursorShower>(Lifetime.Singleton);
}
