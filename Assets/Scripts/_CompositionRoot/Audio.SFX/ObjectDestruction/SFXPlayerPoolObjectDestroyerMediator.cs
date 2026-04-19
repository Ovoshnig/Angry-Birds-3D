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
        _destroyer.Collided.Merge(_destroyer.Damaged)
            .Where((_, index) => index % 6 == 0)
            .Subscribe(OnDamaged)
            .AddTo(CompositeDisposable);

        _destroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(CompositeDisposable);
    }

    private void OnDamaged(DamageEvent<TView> damageEvent)
    {
        Vector3 position = damageEvent.EntityView.transform.position;
        AudioResource audioResource = damageEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }

    private void OnDestroyed(DestructionEvent<TView> destructionEvent)
    {
        Vector3 position = destructionEvent.EntityView.transform.position;
        AudioResource audioResource = destructionEvent.AudioResource;
        _playerObjectPool.PlaySFX(position, audioResource);
    }
}
