using R3;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerPoolObjectDestroyerMediator<TView> : Mediator where TView : MonoBehaviour
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly ObjectDestroyer<TView> _destroyer;

    public SFXPlayerPoolObjectDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
        ObjectDestroyer<TView> destroyer)
    {
        _playerObjectPool = playerObjectPool;
        _destroyer = destroyer;
    }

    public override void Initialize()
    {
        _destroyer.Damaged
            .Where((_, index) => index % 6 == 0)
            .Subscribe(OnDamaged)
            .AddTo(CompositeDisposable);

        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(CompositeDisposable);
    }

    private void OnDamaged(DamageEvent<TView> damageEvent)
    {
        Vector3 position = damageEvent.DestroyerView.transform.position;
        DestructionSFXSettings sfxSettings = damageEvent.DestroyerView.Settings.SfxSettings;

        AudioResource audioResource = damageEvent.CollisionType switch
        {
            CollisionType.Gliding => sfxSettings.GlidingResource,
            CollisionType.Collision => sfxSettings.CollisionResource,
            CollisionType.Damage => sfxSettings.DamageResource,
            _ => sfxSettings.DestructionResource
        };

        _playerObjectPool.PlaySFX(position, audioResource);
    }

    private void OnDestroyed(DestructionEvent<TView> destructionEvent)
    {
        ObjectDestroyerView destroyerView = destructionEvent.DestroyerView;
        Vector3 position = destroyerView.transform.position;
        AudioResource audioResource = destroyerView.Settings.SfxSettings.DestructionResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }
}
