using R3;

public class BlockDestroyerMediator : Mediator
{
    private readonly BlockDestroyer _blockDestroyer;

    public BlockDestroyerMediator(BlockDestroyer blockDestroyer) => _blockDestroyer = blockDestroyer;

    public override void Initialize()
    {
        _blockDestroyer.Damaged
            .Subscribe(blockDamageEvent =>
            blockDamageEvent.BlockDestroyerView.Damage(blockDamageEvent.Damage))
            .AddTo(CompositeDisposable);

        _blockDestroyer.Destroyed
            .Subscribe(blockDestructionEvent => blockDestructionEvent.BlockDestroyerView.Destroy())
            .AddTo(CompositeDisposable);
    }
}
