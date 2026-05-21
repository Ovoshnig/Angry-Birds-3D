using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelLifetimeScope : LifetimeScope
{
    [SerializeField] private SceneInstaller _sceneInstaller;
    [SerializeField] private LevelStateInstaller _levelStateInstaller;
    [SerializeField] private LevelScoreInstaller _levelScoreInstaller;
    [SerializeField] private CameraInstaller _cameraInstaller;
    [SerializeField] private AudioSFXInstaller _sfxInstaller;
    [SerializeField] private SlingshotInstaller _slingshotInstaller;
    [SerializeField] private ObjectCollisionInstaller _objectCollisionInstaller;
    [SerializeField] private ObjectDestructionInstaller _objectDestructionInstaller;
    [SerializeField] private BirdInstaller _birdInstaller;
    [SerializeField] private PigInstaller _pigInstaller;
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
        _slingshotInstaller.Install(builder);
        _objectCollisionInstaller.Install(builder);
        _objectDestructionInstaller.Install(builder);
        _birdInstaller.Install(builder);
        _pigInstaller.Install(builder);
        _blockInstaller.Install(builder);
        InstallCollidableEntities(builder);
    }

    private void InstallMediators(IContainerBuilder builder)
    {
        new LevelStateMediatorsInstaller().Install(builder);
        new ScoreMediatorsInstaller().Install(builder);
        new CameraMediatorsInstaller().Install(builder);
        new AudioSFXMediatorsInstaller().Install(builder);
        new BirdMediatorsInstaller().Install(builder);
        new SlingshotMediatorsInstaller().Install(builder);
    }

    private void InstallCollidableEntities(IContainerBuilder builder)
    {
        builder.Register(resolver =>
        {
            IReadOnlyList<BirdEntityView> birds = resolver.Resolve<IReadOnlyList<BirdEntityView>>();
            IReadOnlyList<BlockEntityView> blocks = resolver.Resolve<IReadOnlyList<BlockEntityView>>();
            IReadOnlyList<PigEntityView> pigs = resolver.Resolve<IReadOnlyList<PigEntityView>>();

            List<CollidableEntityView> allCollidables = new(birds.Count + blocks.Count + pigs.Count);
            allCollidables.AddRange(birds);
            allCollidables.AddRange(blocks);
            allCollidables.AddRange(pigs);

            return allCollidables;
        },
        Lifetime.Singleton).As<IReadOnlyList<CollidableEntityView>>();
    }
}
