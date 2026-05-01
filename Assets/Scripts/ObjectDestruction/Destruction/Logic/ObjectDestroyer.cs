using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public record DamageEvent<TView>(ObjectDestroyerView DestroyerView, CollisionType CollisionType,
    float Damage) where TView : MonoBehaviour;

public record DestructionEvent<TView>(ObjectDestroyerView DestroyerView)
    where TView : MonoBehaviour;

public abstract class ObjectDestroyer<TView> : IStartable, IDisposable
    where TView : MonoBehaviour
{
    private readonly ObjectCollider<TView> _objectCollider;
    private readonly Subject<DamageEvent<TView>> _damaged = new();
    private readonly Subject<DestructionEvent<TView>> _destroyed = new();
    private readonly CompositeDisposable _disposables = new();

    public ObjectDestroyer(ObjectCollider<TView> objectCollider) =>
        _objectCollider = objectCollider;

    public Observable<DamageEvent<TView>> Damaged => _damaged;
    public Observable<DestructionEvent<TView>> Destroyed => _destroyed;

    public void Start()
    {
        _objectCollider.Collided
            .Subscribe(OnCollided)
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();

        _damaged.Dispose();
        _destroyed.Dispose();
    }

    protected abstract ObjectDestroyerView GetObjectDestroyerView(TView entityView);

    private void OnCollided(CollisionEvent<TView> collisionEvent)
    {
        TView entityView = collisionEvent.View;
        ObjectDestroyerView destroyerView = GetObjectDestroyerView(entityView);

        float damage = collisionEvent.Force;
        destroyerView.HealthModel.Decrement(damage);

        if (destroyerView.HealthModel.Health <= 0)
        {
            destroyerView.Destroy();
            _destroyed.OnNext(new DestructionEvent<TView>(destroyerView));
        }
        else
        {
            destroyerView.Damage(damage);
            _damaged.OnNext(new DamageEvent<TView>(destroyerView, collisionEvent.Type, damage));
        }
    }
}
