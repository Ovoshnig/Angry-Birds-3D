using R3;

public class BlockDestroyerMediator : Mediator
{
    private readonly BlockDestroyer _blockDestroyer;

    public BlockDestroyerMediator(BlockDestroyer blockDestroyer) => _blockDestroyer = blockDestroyer;

    public override void Initialize()
    {
        _blockDestroyer.Damaged
            .Subscribe(blockDamageEvent =>
            blockDamageEvent.EntityView.DestroyerView.Damage(blockDamageEvent.Damage))
            .AddTo(CompositeDisposable);

        _blockDestroyer.Destroyed
            .Subscribe(blockDestructionEvent =>
            blockDestructionEvent.EntityView.DestroyerView.Destroy())
            .AddTo(CompositeDisposable);
    }
}
