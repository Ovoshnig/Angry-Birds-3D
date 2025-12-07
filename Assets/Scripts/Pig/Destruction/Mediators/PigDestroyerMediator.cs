using R3;

public class PigDestroyerMediator : Mediator
{
    private readonly PigDestroyer _pigDestroyer;

    public PigDestroyerMediator(PigDestroyer pigDestroyer) => _pigDestroyer = pigDestroyer;

    public override void Initialize()
    {
        _pigDestroyer.Damaged
            .Subscribe(damageEvent =>
            damageEvent.EntityView.DestroyerView.Damage(damageEvent.Damage))
            .AddTo(CompositeDisposable);

        _pigDestroyer.Destroyed
            .Subscribe(destructionEvent =>
            destructionEvent.EntityView.DestroyerView.Destroy())
            .AddTo(CompositeDisposable);
    }
}
