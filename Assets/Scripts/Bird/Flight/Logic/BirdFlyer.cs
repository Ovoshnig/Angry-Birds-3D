using R3;
using System;

public class BirdFlyer : IDisposable
{
    private readonly Subject<Unit> _birdCollided = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public Observable<Unit> BirdCollided => _birdCollided;

    public void Dispose() => _compositeDisposable.Dispose();

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(onNext: _ =>
            birdEntityView.FlyerView.LookAtVelocityDirection(),
            onCompleted: result =>
            {
                if (result.IsSuccess)
                    _birdCollided.OnNext(Unit.Default);
            })
            .AddTo(_compositeDisposable);
    }
}
