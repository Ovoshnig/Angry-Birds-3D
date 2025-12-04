using R3;
using System;
using VContainer.Unity;

public record BlockDamageEvent(BlockDestroyerView BlockDestroyerView, float RawDamage);

public class BlockDestroyer : IInitializable, IDisposable
{
    private readonly BlockCollisionReporter _blockCollisionReporter;
    private readonly Subject<BlockDamageEvent> _damaged = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BlockDestroyer(BlockCollisionReporter blockCollisionReporter) =>
        _blockCollisionReporter = blockCollisionReporter;

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
        BlockDestroyerView blockDestroyerView = collisionEvent.View;
        float rawDamage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        BlockDamageEvent damageEvent = new(blockDestroyerView, rawDamage);
        _damaged.OnNext(damageEvent);
    }
}
