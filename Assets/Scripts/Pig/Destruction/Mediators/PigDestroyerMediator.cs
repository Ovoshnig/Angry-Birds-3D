using R3;

public class PigDestroyerMediator : Mediator
{
    private readonly PigDestroyer _pigDestroyer;
    private readonly PigDestroyerView _pigDestroyerView;

    public PigDestroyerMediator(PigDestroyer pigDestroyer,
        PigDestroyerView pigDestroyerView)
    {
        _pigDestroyer = pigDestroyer;
        _pigDestroyerView = pigDestroyerView;
    }

    public override void Initialize()
    {
        _pigDestroyer.Damaged
            .Subscribe(pigDamageEvent =>
            pigDamageEvent.PigDestroyerView.Damage(pigDamageEvent.Damage))
            .AddTo(CompositeDisposable);

        _pigDestroyer.Destroyed
            .Subscribe(_ => _pigDestroyerView.Destroy())
            .AddTo(CompositeDisposable);
    }
}
