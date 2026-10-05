using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Cursor.Showing
{
    [Serializable]
    public class CursorShowingInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.Register<CursorShower>(Lifetime.Singleton);
    }
}
