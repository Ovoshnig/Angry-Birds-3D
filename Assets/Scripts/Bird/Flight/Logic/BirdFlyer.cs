using Cysharp.Threading.Tasks;
using R3;

public class BirdFlyer
{
    private readonly BirdStretchSettings _stretchSettings;
    private readonly Subject<BirdEntityView> _flightInterrupted = new();

    public BirdFlyer(BirdStretchSettings stretchSettings) => _stretchSettings = stretchSettings;

    public Observable<BirdEntityView> FlightInterrupted => _flightInterrupted;

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        BirdFlyerView flyerView = birdEntityView.FlyerView;
        flyerView.StretchAsync(_stretchSettings).Forget();

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(_ => flyerView.LookAtVelocityDirection(),
                _ => _flightInterrupted.OnNext(birdEntityView))
            .RegisterTo(birdEntityView.destroyCancellationToken);
    }
}
