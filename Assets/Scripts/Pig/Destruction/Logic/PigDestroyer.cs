using R3;
using System;
using VContainer.Unity;

public record PigDamageEvent(PigEntityView EntityView, float Damage);
public record PigDestructionEvent(PigEntityView EntityView, int Points);

public class PigDestroyer : IInitializable, IDisposable
{
    private readonly PigCollisionReporter _pigCollisionReporter;
    private readonly PigSettings _pigSettings;
    private readonly ScoreSettings _scoreSettings;
    private readonly Subject<PigDamageEvent> _collided = new();
    private readonly Subject<PigDamageEvent> _damaged = new();
    private readonly Subject<PigDestructionEvent> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public PigDestroyer(PigCollisionReporter pigCollisionReporter,
        PigSettings pigSettings,
        ScoreSettings scoreSettings)
    {
        _pigCollisionReporter = pigCollisionReporter;
        _pigSettings = pigSettings;
        _scoreSettings = scoreSettings;
    }

    public Observable<PigDamageEvent> Collided => _collided;
    public Observable<PigDamageEvent> Damaged => _damaged;
    public Observable<PigDestructionEvent> Destroyed => _destroyed;

    public void Initialize()
    {
        _pigCollisionReporter.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void OnCollided(CollisionEvent<PigEntityView> collisionEvent)
    {
        PigEntityView entityView = collisionEvent.View;
        PigDestroyerView destroyerView = entityView.DestroyerView;

        float health = destroyerView.HealthModel.Health;
        float damage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        float resultHealth = health - damage;

        if (resultHealth <= 0)
        {
            destroyerView.HealthModel.Decrement(health);
            _destroyed.OnNext(new PigDestructionEvent(entityView, _scoreSettings.PigPoints));
        }
        else
        {
            destroyerView.HealthModel.Decrement(damage);

            if (damage < _pigSettings.DamageThreshold)
                _collided.OnNext(new PigDamageEvent(entityView, damage));
            else
                _damaged.OnNext(new PigDamageEvent(entityView, damage));
        }
    }
}
