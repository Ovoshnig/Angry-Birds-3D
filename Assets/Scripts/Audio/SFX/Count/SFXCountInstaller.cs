using System;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Audio.SFX.Count
{
    [Serializable]
    public class SFXCountInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder) => builder.Register<SFXCounter>(Lifetime.Singleton);
    }
}
