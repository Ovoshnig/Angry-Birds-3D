using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneInstaller _sceneInstaller;
    [SerializeField] private LevelStateInstaller _levelStateInstaller;
    [SerializeField] private LevelScoreInstaller _levelScoreInstaller;
    [SerializeField] private CameraInstaller _cameraInstaller;
    [SerializeField] private SFXInstaller _sfxInstaller;
    [SerializeField] private BirdInstaller _birdInstaller;
    [SerializeField] private PigInstaller _pigInstaller;
    [SerializeField] private SlingshotInstaller _slingshotInstaller;
    [SerializeField] private BlockInstaller _blockInstaller;

    protected override void Configure(IContainerBuilder builder)
    {
        InstallSystems(builder);
        InstallMediators(builder);
    }

    private void InstallSystems(IContainerBuilder builder)
    {
        _sceneInstaller.Install(builder);
        _levelStateInstaller.Install(builder);
        _levelScoreInstaller.Install(builder);
        _cameraInstaller.Install(builder);
        _sfxInstaller.Install(builder);
        _birdInstaller.Install(builder);
        _pigInstaller.Install(builder);
        _slingshotInstaller.Install(builder);
        _blockInstaller.Install(builder);
    }

    private static void InstallMediators(IContainerBuilder builder)
    {
        new ScoreMediatorsInstaller().Install(builder);
        new CameraMediatorsInstaller().Install(builder);
        new SFXMediatorsInstaller().Install(builder);
        new BirdMediatorsInstaller().Install(builder);
        new SlingshotMediatorsInstaller().Install(builder);
    }
}
