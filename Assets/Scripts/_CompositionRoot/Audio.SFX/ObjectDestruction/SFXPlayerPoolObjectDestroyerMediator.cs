using R3;
using System.Linq;
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

    public override void Start()
    {
        _destroyer.Damaged
            .Where((damageEvent, index) => damageEvent.DestroyerView is not BlockDestroyerView
                || index % 6 == 0)
            .Subscribe(OnDamaged)
            .AddTo(Disposables);

        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(Disposables);
    }

    private void OnDamaged(DamageEvent damageEvent)
    {
        Transform target = damageEvent.DestroyerView.transform;
        DestructionSFXSettings sfxSettings = damageEvent.DestroyerView.Settings.SfxSettings;

        AudioResource audioResource = damageEvent.CollisionType switch
        {
            CollisionType.Gliding => sfxSettings.GlidingResource,
            CollisionType.Collision => sfxSettings.CollisionResource,
            CollisionType.Damage => sfxSettings.DamageResource,
            _ => sfxSettings.DestructionResource
        };

        _playerObjectPool.PlaySFX(target, audioResource);
    }

    private void OnDestroyed(DestructionEvent destructionEvent)
    {
        ObjectDestroyerView destroyerView = destructionEvent.DestroyerView;
        Transform target = destroyerView.transform;
        AudioResource audioResource = destroyerView.Settings.SfxSettings.DestructionResource;
        _playerObjectPool.PlaySFX(target, audioResource);
    }
}
