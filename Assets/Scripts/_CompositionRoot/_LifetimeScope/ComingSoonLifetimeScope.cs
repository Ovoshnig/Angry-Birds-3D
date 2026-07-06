using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ComingSoonLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneSwitchingInstaller _sceneSwitchingInstaller;

    protected override void Configure(IContainerBuilder builder) => _sceneSwitchingInstaller.Install(builder);
}
