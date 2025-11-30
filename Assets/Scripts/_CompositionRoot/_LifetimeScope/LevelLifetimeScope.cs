using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    [SerializeField] private LevelStateInstaller _levelStateInstaller;
    [SerializeField] private BirdInstaller _birdInstaller;
    [SerializeField] private SlingshotInstaller _slingshotInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        new CompositionRootInstaller().Install(builder);

        _levelStateInstaller.Install(builder);
        _birdInstaller.Install(builder);
        _slingshotInstaller.Install(builder);
    }
}
