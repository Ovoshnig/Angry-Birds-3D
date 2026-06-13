using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;

public class BirdFlyer : IDisposable
{
    private readonly BirdStretchSettings _stretchSettings;
    private readonly Subject<BirdEntityView> _flightStarted = new();
    private readonly Subject<BirdEntityView> _flightInterrupted = new();

    public BirdFlyer(BirdStretchSettings stretchSettings) => _stretchSettings = stretchSettings;

    public Observable<BirdEntityView> FlightStarted => _flightStarted;
    public Observable<BirdEntityView> FlightInterrupted => _flightInterrupted;

    public void Dispose()
    {
        _flightStarted.Dispose();
        _flightInterrupted.Dispose();
    }

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        _flightStarted.OnNext(birdEntityView);

        CancellationTokenSource flightCts = CancellationTokenSource
            .CreateLinkedTokenSource(birdEntityView.destroyCancellationToken);
        birdEntityView.destroyCancellationToken.Register(() => flightCts.Dispose());

        BirdFlyerView flyerView = birdEntityView.FlyerView;
        flyerView.StretchAsync(_stretchSettings, flightCts.Token).Forget();

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(_ => flyerView.LookAtVelocityDirection(),
                result =>
                {
                    flightCts.Cancel();
                    _flightInterrupted.OnNext(birdEntityView);
                })
            .RegisterTo(birdEntityView.destroyCancellationToken);
    }
}
