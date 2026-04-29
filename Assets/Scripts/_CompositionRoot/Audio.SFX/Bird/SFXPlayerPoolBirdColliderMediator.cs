using R3;

public class SFXPlayerPoolBirdColliderMediator : Mediator
{
    private readonly SFXPlayerObjectPool _playerObjectPool;
    private readonly BirdCollider _birdCollider;

    public SFXPlayerPoolBirdColliderMediator(SFXPlayerObjectPool playerObjectPool,
        BirdCollider birdCollider)
    {
        _playerObjectPool = playerObjectPool;
        _birdCollider = birdCollider;
    }

    public override void Start()
    {
        _birdCollider.Collided
            .Subscribe(@event =>
            {
                if (@event.Type == CollisionType.Damage)
                    _playerObjectPool.PlaySFX(@event.View.transform, @event.View.SFXSettings.CollisionResource);
            })
            .AddTo(CompositeDisposable);
    }
}
