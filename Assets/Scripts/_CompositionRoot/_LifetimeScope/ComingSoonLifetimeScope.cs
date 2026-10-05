using Ovoshnig.Scene.Switching;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.Composition
{
    public class ComingSoonLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;
    
        protected override void Configure(IContainerBuilder builder) => _sceneSwitchingInstaller.Install(builder);
    }
}
