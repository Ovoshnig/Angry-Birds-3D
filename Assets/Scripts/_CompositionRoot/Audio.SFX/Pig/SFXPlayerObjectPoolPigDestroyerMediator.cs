using R3;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerObjectPoolPigDestroyerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly PigDestroyer _pigDestroyer;

    public SFXPlayerObjectPoolPigDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
        PigDestroyer pigDestroyer)
    {
        _playerObjectPool = playerObjectPool;
        _pigDestroyer = pigDestroyer;
    }

    public override void Initialize()
    {
        _pigDestroyer.Collided
            .Where((_, index) => index % 2 != 0)
            .Subscribe(OnDamaged)
            .AddTo(CompositeDisposable);

        _pigDestroyer.Damaged
            .Where((_, index) => index % 2 != 0)
            .Subscribe(OnDamaged)
            .AddTo(CompositeDisposable);

        _pigDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(CompositeDisposable);
    }

    private void OnDamaged(PigDamageEvent damageEvent)
    {
        Vector3 position = damageEvent.EntityView.transform.position;
        AudioResource audioResource = damageEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }

    private void OnDestroyed(PigDestructionEvent destructionEvent)
    {
        Vector3 position = destructionEvent.EntityView.transform.position;
        AudioResource audioResource = destructionEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }
}
