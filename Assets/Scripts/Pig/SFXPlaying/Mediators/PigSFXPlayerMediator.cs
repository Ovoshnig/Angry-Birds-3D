using R3;

public class PigSFXPlayerMediator : Mediator
{
    private readonly PigDestroyer _pigDestroyer;

    public PigSFXPlayerMediator(PigDestroyer pigDestroyer) =>
        _pigDestroyer = pigDestroyer;

    public override void Initialize()
    {
        _pigDestroyer.Collided
            .Subscribe(damageEvent =>
            damageEvent.PigDestroyerView.SFXPlayerView.PlayCollision())
            .AddTo(CompositeDisposable);

        _pigDestroyer.Damaged
            .Subscribe(damageEvent =>
            damageEvent.PigDestroyerView.SFXPlayerView.PlayDamage())
            .AddTo(CompositeDisposable);

        _pigDestroyer.Destroyed
            .Subscribe(destroyerView =>
            destroyerView.SFXPlayerView.PlayDestruction())
            .AddTo(CompositeDisposable);
    }
}
