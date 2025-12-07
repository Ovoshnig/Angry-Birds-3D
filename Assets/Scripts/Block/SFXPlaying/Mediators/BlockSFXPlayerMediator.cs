using R3;

public class BlockSFXPlayerMediator : Mediator
{
    private readonly BlockDestroyer _blockDestroyer;

    public BlockSFXPlayerMediator(BlockDestroyer blockDestroyer) =>
        _blockDestroyer = blockDestroyer;

    public override void Initialize()
    {
        _blockDestroyer.Collided
            .Subscribe(damageEvent =>
            damageEvent.BlockDestroyerView.SFXPlayerView.PlayCollision())
            .AddTo(CompositeDisposable);

        _blockDestroyer.Damaged
            .Subscribe(damageEvent =>
            damageEvent.BlockDestroyerView.SFXPlayerView.PlayDamage())
            .AddTo(CompositeDisposable);

        _blockDestroyer.Destroyed
            .Subscribe(destructionEvent =>
            destructionEvent.BlockDestroyerView.SFXPlayerView.PlayDestruction())
            .AddTo(CompositeDisposable);
    }
}
