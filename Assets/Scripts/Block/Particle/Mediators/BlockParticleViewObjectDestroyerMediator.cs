using R3;
using UnityEngine;

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
        if (data.EntityView is not BlockEntityView entityView)
            return;

        Vector3 position = entityView.transform.position;
        float force = data.DamageAmount;
        BlockParticleProfile particleProfile = entityView.ParticleProfile;

        _blockParticleView.Emit(position, force, particleProfile);
    }

    private void OnDestroyed(DestructionData data)
    {
        if (data.EntityView is not BlockEntityView entityView)
            return;

        Vector3 position = entityView.transform.position;
        BlockParticleProfile particleProfile = entityView.ParticleProfile;
        int count = particleProfile.MaxParticles;

        _blockParticleView.Emit(position, count, particleProfile);
    }
}
