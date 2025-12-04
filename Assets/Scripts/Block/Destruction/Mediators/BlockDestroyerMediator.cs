using R3;

public class BlockDestroyerMediator : Mediator
{
    private readonly BlockDestroyer _blockDestroyer;

    public override void Initialize()
    {
        _blockDestroyer.Damaged
            .Subscribe(blockDamageEvent =>
            blockDamageEvent.BlockDestroyerView.Damage(blockDamageEvent.Damage))
            .AddTo(CompositeDisposable);
    }
}
