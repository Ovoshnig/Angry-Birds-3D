using R3;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerPoolObjectDestroyerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly ObjectDestroyer _destroyer;

    public SFXPlayerPoolObjectDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
        ObjectDestroyer destroyer)
    {
        _playerObjectPool = playerObjectPool;
        _destroyer = destroyer;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _destroyer.Damaged
            .Subscribe(OnDamaged)
            .AddTo(disposables);

        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(disposables);
    }

    private void OnDamaged(DamageData data)
    {
        Transform target = data.EntityView.transform;
        DestructionSfxProfile SfxProfile = data.EntityView.DestructionProfile.SfxProfile;

        AudioResource audioResource = data.CollisionType switch
        {
            CollisionType.Gliding => SfxProfile.GlidingResource,
            CollisionType.Collision => SfxProfile.CollisionResource,
            CollisionType.Damage => SfxProfile.DamageResource,
            _ => SfxProfile.DestructionResource
        };

        _playerObjectPool.PlaySFX(target, audioResource);
    }

    private void OnDestroyed(DestructionData data)
    {
        DestructibleEntityView entityView = data.EntityView;
        Transform target = entityView.transform;
        AudioResource audioResource = entityView.DestructionProfile.SfxProfile.DestructionResource;
        _playerObjectPool.PlaySFX(target, audioResource);
    }
}
