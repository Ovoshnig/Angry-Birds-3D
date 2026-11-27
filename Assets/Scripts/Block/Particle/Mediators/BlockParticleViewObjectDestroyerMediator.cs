using AngryBirds3D.Block.Entity;
using Ovoshnig.Mediation;
using Ovoshnig.ObjectDestruction.Destruction;
using Ovoshnig.ObjectDestruction.Entity;
using R3;
using UnityEngine;

namespace AngryBirds3D.Block.Particle;

public class BlockParticleViewObjectDestroyerMediator : Mediator
{
    private readonly BlockParticleView _blockParticleView;
    private readonly ObjectDestroyer _objectDestroyer;

    public BlockParticleViewObjectDestroyerMediator(BlockParticleView blockParticleView,
        ObjectDestroyer objectDestroyer)
    {
        _blockParticleView = blockParticleView;
        _objectDestroyer = objectDestroyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _objectDestroyer.Damaged
            .Subscribe(OnDamaged)
            .AddTo(disposables);

        _objectDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(disposables);
    }

    private void OnDamaged(DamageData data)
    {
        if (data.EntityView is not BlockEntityView blockEntityView)
            return;

        Vector3 position = blockEntityView.transform.position;
        float force = data.DamageAmount;
        BlockParticleProfile particleProfile = blockEntityView.ParticleProfile;

        _blockParticleView.Emit(position, force, particleProfile);
    }

    private void OnDestroyed(DestructibleEntityView entityView)
    {
        if (entityView is not BlockEntityView blockEntityView)
            return;

        Vector3 position = blockEntityView.transform.position;
        BlockParticleProfile particleProfile = blockEntityView.ParticleProfile;
        int count = particleProfile.MaxParticles;

        _blockParticleView.Emit(position, count, particleProfile);
    }
}
