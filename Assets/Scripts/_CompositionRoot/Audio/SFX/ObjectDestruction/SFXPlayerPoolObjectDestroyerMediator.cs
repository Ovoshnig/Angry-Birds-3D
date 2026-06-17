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
        Transform target = data.DestroyerView.transform;
        DestructionSFXSettings sfxSettings = data.DestroyerView.Settings.SfxSettings;

        AudioResource audioResource = data.CollisionType switch
        {
            CollisionType.Gliding => sfxSettings.GlidingResource,
            CollisionType.Collision => sfxSettings.CollisionResource,
            CollisionType.Damage => sfxSettings.DamageResource,
            _ => sfxSettings.DestructionResource
        };

        _playerObjectPool.PlaySFX(target, audioResource);
    }

    private void OnDestroyed(DestructionData data)
    {
        ObjectDestroyerView destroyerView = data.DestroyerView;
        Transform target = destroyerView.transform;
        AudioResource audioResource = destroyerView.Settings.SfxSettings.DestructionResource;
        _playerObjectPool.PlaySFX(target, audioResource);
    }
}
