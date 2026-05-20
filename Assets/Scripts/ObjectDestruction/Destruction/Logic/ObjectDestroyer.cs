using R3;
using System;
using VContainer.Unity;

public record DamageEvent(DestructibleEntityView EntityView, ObjectDestroyerView DestroyerView,
    CollisionType CollisionType, float DamageAmount);

public record DestructionEvent(DestructibleEntityView EntityView, ObjectDestroyerView DestroyerView);

public class ObjectDestroyer : IStartable, IDisposable
{
    private readonly ObjectCollider _objectCollider;
    private readonly Subject<DamageEvent> _damaged = new();
    private readonly Subject<DestructionEvent> _destroyed = new();
    private readonly CompositeDisposable _disposables = new();

    public ObjectDestroyer(ObjectCollider objectCollider) =>
        _objectCollider = objectCollider;

    public Observable<DamageEvent> Damaged => _damaged;
    public Observable<DestructionEvent> Destroyed => _destroyed;

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

    private void OnCollided(CollisionEvent collisionEvent)
    {
        if (collisionEvent.EntityView is not DestructibleEntityView entityView)
            return;

        ObjectDestroyerView destroyerView = entityView.DestroyerView;

        float damageAmount = collisionEvent.Force;
        destroyerView.HealthModel.ApplyDamage(damageAmount);

        if (destroyerView.HealthModel.Health <= 0)
        {
            destroyerView.Destroy();
            _destroyed.OnNext(new DestructionEvent(entityView, destroyerView));
        }
        else
        {
            destroyerView.Damage(damageAmount);
            _damaged.OnNext(new DamageEvent(entityView, destroyerView,
                collisionEvent.Type, damageAmount));
        }
    }
}
