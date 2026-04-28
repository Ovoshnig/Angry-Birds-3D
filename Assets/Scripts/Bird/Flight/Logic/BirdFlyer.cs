using R3;

public class BirdFlyer
{
    private readonly Subject<Unit> _birdCollided = new();

    public Observable<Unit> BirdCollided => _birdCollided;

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(onNext: _ => birdEntityView.FlyerView.LookAtVelocityDirection(),
            onCompleted: result =>
            {
                if (result.IsSuccess)
                    _birdCollided.OnNext(Unit.Default);
            })
            .AddTo(birdEntityView);
    }
}
