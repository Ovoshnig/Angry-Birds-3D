using R3;

public class BirdFlyer
{
    private readonly Subject<BirdEntityView> _flightInterrupted = new();

    public Observable<BirdEntityView> FlightInterrupted => _flightInterrupted;

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(_ => birdEntityView.FlyerView.LookAtVelocityDirection(),
                _ => _flightInterrupted.OnNext(birdEntityView))
            .RegisterTo(birdEntityView.destroyCancellationToken);
    }
}
