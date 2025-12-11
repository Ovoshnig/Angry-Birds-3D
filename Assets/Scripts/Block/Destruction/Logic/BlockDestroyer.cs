using R3;
using System;
using UnityEngine.Audio;
using VContainer.Unity;

public record BlockDamageEvent(BlockEntityView EntityView, float Damage, AudioResource AudioResource);
public record BlockDestructionEvent(BlockEntityView EntityView, DestructionPointsSettings PointsSettings, 
    AudioResource AudioResource);

public class BlockDestroyer : IInitializable, IDisposable
{
    private readonly BlockCollisionReporter _blockCollisionReporter;
    private readonly BlockSettings _blockSettings;
    private readonly ScoreSettings _scoreSettings;
    private readonly Subject<BlockDamageEvent> _collided = new();
    private readonly Subject<BlockDamageEvent> _damaged = new();
    private readonly Subject<BlockDestructionEvent> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BlockDestroyer(BlockSettings blockSettings,
        BlockCollisionReporter blockCollisionReporter,
        ScoreSettings gameSettings)
    {
        _blockSettings = blockSettings;
        _blockCollisionReporter = blockCollisionReporter;
        _scoreSettings = gameSettings;
    }

    public Observable<BlockDamageEvent> Collided => _collided;
    public Observable<BlockDamageEvent> Damaged => _damaged;
    public Observable<BlockDestructionEvent> Destroyed => _destroyed;

    public void Initialize()
    {
        _blockCollisionReporter.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void OnCollided(CollisionEvent<BlockEntityView> collisionEvent)
    {
        BlockEntityView entityView = collisionEvent.View;
        BlockDestroyerView destroyerView = entityView.DestroyerView;
        DestructionSFXSettings sfxSettings = destroyerView.DestructionSFXSettings;

        float health = destroyerView.HealthModel.Health;
        float damage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        float resultHealth = health - damage;

        if (resultHealth <= 0)
        {
            destroyerView.HealthModel.Decrement(health);
            _destroyed.OnNext(new BlockDestructionEvent(entityView,
                _scoreSettings.BlockPointsSettings, 
                sfxSettings.DestructionResource));
        }
        else
        {
            destroyerView.HealthModel.Decrement(damage);

            if (damage < _blockSettings.DamageThreshold)
                _collided.OnNext(new BlockDamageEvent(entityView, damage, sfxSettings.CollisionResource));
            else
                _damaged.OnNext(new BlockDamageEvent(entityView, damage, sfxSettings.DamageResource));
        }
    }
}
