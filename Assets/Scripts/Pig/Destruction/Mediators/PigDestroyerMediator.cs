using R3;

public class PigDestroyerMediator : Mediator
{
    private readonly PigDestroyer _pigDestroyer;

    public PigDestroyerMediator(PigDestroyer pigDestroyer) => _pigDestroyer = pigDestroyer;

    public override void Initialize()
    {
        _pigDestroyer.Damaged
            .Subscribe(pigDamageEvent =>
            pigDamageEvent.PigDestroyerView.Damage(pigDamageEvent.Damage))
            .AddTo(CompositeDisposable);

        _pigDestroyer.Destroyed
            .Subscribe(pigDestroyerView => pigDestroyerView.Destroy())
            .AddTo(CompositeDisposable);
    }
}
