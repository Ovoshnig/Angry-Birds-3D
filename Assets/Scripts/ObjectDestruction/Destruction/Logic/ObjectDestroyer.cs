using R3;
using System;
using UnityEngine;
using UnityEngine.Audio;
using VContainer.Unity;

public record DamageEvent<TView>(TView EntityView, float Damage, AudioResource AudioResource)
    where TView : MonoBehaviour;

public record DestructionEvent<TView>(TView EntityView, DestructionPointsSettings PointsSettings,
    AudioResource AudioResource) where TView : MonoBehaviour;

public abstract class ObjectDestroyer<TView> : IInitializable, IDisposable
    where TView : MonoBehaviour
{
    private readonly ObjectCollider<TView> _objectCollider;
    private readonly Subject<DamageEvent<TView>> _collided = new();
    private readonly Subject<DamageEvent<TView>> _damaged = new();
    private readonly Subject<DestructionEvent<TView>> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public ObjectDestroyer(ObjectCollider<TView> objectCollider) =>
        _objectCollider = objectCollider;

    public Observable<DamageEvent<TView>> Collided => _collided;
    public Observable<DamageEvent<TView>> Damaged => _damaged;
    public Observable<DestructionEvent<TView>> Destroyed => _destroyed;

    protected abstract DestructionPointsSettings DestructionPointsSettings { get; }
    protected abstract float DamageThreshold { get; }

    public void Initialize()
    {
        _objectCollider.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    public abstract ObjectDestroyerView GetObjectDestroyerView(TView entityView);

    private void OnCollided(CollisionEvent<TView> collisionEvent)
    {
        TView entityView = collisionEvent.View;
        ObjectDestroyerView destroyerView = GetObjectDestroyerView(entityView);
        DestructionSFXSettings sfxSettings = destroyerView.DestructionSFXSettings;

        float health = destroyerView.HealthModel.Health;
        float damage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        float resultHealth = health - damage;

        if (resultHealth <= 0)
        {
            destroyerView.HealthModel.Decrement(health);

            _destroyed.OnNext(new DestructionEvent<TView>(entityView,
                DestructionPointsSettings,
                sfxSettings.DestructionResource));
        }
        else
        {
            destroyerView.HealthModel.Decrement(damage);

            if (damage < DamageThreshold)
                _collided.OnNext(new DamageEvent<TView>(entityView, damage, sfxSettings.CollisionResource));
            else
                _damaged.OnNext(new DamageEvent<TView>(entityView, damage, sfxSettings.DamageResource));
        }
    }
}
