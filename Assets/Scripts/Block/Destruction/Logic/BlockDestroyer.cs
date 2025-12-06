using R3;
using System;
using VContainer.Unity;

public record BlockDamageEvent(BlockDestroyerView BlockDestroyerView, float Damage);
public record BlockDestructionEvent(BlockDestroyerView BlockDestroyerView, int Points);

public class BlockDestroyer : IInitializable, IDisposable
{
    private readonly BlockCollisionReporter _blockCollisionReporter;
    private readonly ScoreSettings _scoreSettings;
    private readonly Subject<BlockDamageEvent> _damaged = new();
    private readonly Subject<BlockDestructionEvent> _destroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BlockDestroyer(BlockCollisionReporter blockCollisionReporter, 
        ScoreSettings gameSettings)
    {
        _blockCollisionReporter = blockCollisionReporter;
        _scoreSettings = gameSettings;
    }

    public Observable<BlockDamageEvent> Damaged => _damaged;
    public Observable<BlockDestructionEvent> Destroyed => _destroyed;

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

        float health = blockDestroyerView.HealthModel.Health;
        float damage = collisionEvent.Collision.relativeVelocity.sqrMagnitude;
        float resultHealth = health - damage;

        if (resultHealth <= 0)
        {
            blockDestroyerView.HealthModel.Decrement(health);
            _destroyed.OnNext(new BlockDestructionEvent(blockDestroyerView, 
                _scoreSettings.BlockPoints));
        }
        else
        {
            blockDestroyerView.HealthModel.Decrement(damage);
            _damaged.OnNext(new BlockDamageEvent(blockDestroyerView, damage));
        }
    }
}
