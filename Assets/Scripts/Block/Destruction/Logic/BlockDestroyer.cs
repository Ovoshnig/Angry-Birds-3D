using R3;
using System;
using VContainer.Unity;

public record BlockDamageEvent(BlockDestroyerView BlockDestroyerView, float Damage);

public class BlockDestroyer : IInitializable, IDisposable
{
    private readonly BlockCollisionReporter _blockCollisionReporter;
    private readonly BlockSettings _blockSettings;
    private readonly Subject<BlockDamageEvent> _damaged = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BlockDestroyer(BlockCollisionReporter blockCollisionReporter,
        BlockSettings blockSettings)
    {
        _blockCollisionReporter = blockCollisionReporter;
        _blockSettings = blockSettings;
    }

    public Observable<BlockDamageEvent> Damaged => _damaged;

    public void Initialize()
    {
        _blockCollisionReporter.Collided
            .Subscribe(OnCollided)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void OnCollided(CollisionEvent<BlockDestroyerView> collisionEvent)
    {
        float damage = _blockSettings.WoodDestructionMultiplier
            * collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        BlockDamageEvent damageEvent = new(collisionEvent.View, damage);
        _damaged.OnNext(damageEvent);
    }
}
