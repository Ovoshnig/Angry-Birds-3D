using R3;
using System;
using VContainer.Unity;

public record PigDamageEvent(PigDestroyerView PigDestroyerView, float Damage);

public class PigDestroyer : IInitializable, IDisposable
{
    private readonly PigCollisionReporter _pigCollisionReporter;
    private readonly PigSettings _pigSettings;
    private readonly Subject<PigDamageEvent> _collided = new();
    private readonly Subject<PigDamageEvent> _damaged = new();
    private readonly Subject<PigDestroyerView> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public PigDestroyer(PigCollisionReporter pigCollisionReporter,
        PigSettings pigSettings)
    {
        _pigCollisionReporter = pigCollisionReporter;
        _pigSettings = pigSettings;
    }

    public Observable<PigDamageEvent> Collided => _collided;
    public Observable<PigDamageEvent> Damaged => _damaged;
    public Observable<PigDestroyerView> Destroyed => _destroyed;

    public void Initialize()
    {
        _pigCollisionReporter.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void OnCollided(CollisionEvent<PigDestroyerView> collisionEvent)
    {
        PigDestroyerView pigDestroyerView = collisionEvent.View;

        float health = pigDestroyerView.HealthModel.Health;
        float damage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        float resultHealth = health - damage;

        if (resultHealth <= 0)
        {
            pigDestroyerView.HealthModel.Decrement(health);
            _destroyed.OnNext(pigDestroyerView);
        }
        else
        {
            pigDestroyerView.HealthModel.Decrement(damage);

            if (damage < _pigSettings.DamageThreshold)
                _collided.OnNext(new PigDamageEvent(pigDestroyerView, damage));
            else
                _damaged.OnNext(new PigDamageEvent(pigDestroyerView, damage));
        }
    }
}
