using R3;

public class SFXPlayerPoolBirdColliderMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly ObjectCollider _objectCollider;

    public SFXPlayerPoolBirdColliderMediator(SFXPlayerObjectPool playerObjectPool,
        ObjectCollider objectCollider)
    {
        _playerObjectPool = playerObjectPool;
        _objectCollider = objectCollider;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _objectCollider.Collided
            .Subscribe(@event =>
            {
                if (@event.EntityView is BirdEntityView entityView && @event.Type == CollisionType.Damage)
                    _playerObjectPool.PlaySFX(entityView.transform, entityView.SFXSettings.CollisionResource);
            })
            .AddTo(disposables);
    }
}
