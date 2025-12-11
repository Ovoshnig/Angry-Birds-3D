using R3;
using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayerObjectPoolBlockDestroyerMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly BlockDestroyer _blockDestroyer;

    public SFXPlayerObjectPoolBlockDestroyerMediator(SFXPlayerObjectPool playerObjectPool,
        BlockDestroyer blockDestroyer)
    {
        _playerObjectPool = playerObjectPool;
        _blockDestroyer = blockDestroyer;
    }

    public override void Initialize()
    {
        _blockDestroyer.Collided.Merge(_blockDestroyer.Damaged)
            .Where((_, index) => index % 6 == 0)
            .Subscribe(OnDamaged)
            .AddTo(CompositeDisposable);

        _blockDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(CompositeDisposable);
    }

    private void OnDamaged(BlockDamageEvent damageEvent)
    {
        Vector3 position = damageEvent.EntityView.transform.position;
        AudioResource audioResource = damageEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }

    private void OnDestroyed(BlockDestructionEvent destructionEvent)
    {
        Vector3 position = destructionEvent.EntityView.transform.position;
        AudioResource audioResource = destructionEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }
}
