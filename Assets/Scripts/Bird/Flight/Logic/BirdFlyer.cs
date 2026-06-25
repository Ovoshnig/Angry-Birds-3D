using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;

public class BirdFlyer : IDisposable
{
    private readonly BirdDestroyer _birdDestroyer;
    private readonly Subject<BirdEntityView> _flightStarted = new();
    private readonly Subject<BirdEntityView> _birdCollided = new();
    private readonly Subject<BirdEntityView> _birdFellOut = new();
    private readonly CancellationTokenSource _cts = new();
    private readonly float _minYPosition;

    public BirdFlyer(BirdDestroyer birdDestroyer, Terrain levelTerrain, BirdSettings birdSettings)
    {
        _birdDestroyer = birdDestroyer;
        _minYPosition = levelTerrain.transform.position.y - birdSettings.FallOutDepth;

        FlightInterrupted = Observable.Merge(_birdCollided, _birdFellOut);
    }

    public Observable<BirdEntityView> FlightStarted => _flightStarted;
    public Observable<BirdEntityView> BirdCollided => _birdCollided;
    public Observable<BirdEntityView> BirdFellOut => _birdFellOut;
    public Observable<BirdEntityView> FlightInterrupted { get; }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _flightStarted.Dispose();
        _birdCollided.Dispose();
        _birdFellOut.Dispose();
    }

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        _flightStarted.OnNext(birdEntityView);
        FlyAsync(birdEntityView).Forget();
    }

    private async UniTask FlyAsync(BirdEntityView birdEntityView)
    {
        using CancellationTokenSource flightCts = CancellationTokenSource
            .CreateLinkedTokenSource(_cts.Token, birdEntityView.destroyCancellationToken);

        BirdFlyerView flyerView = birdEntityView.FlyerView;
        flyerView.StretchAsync(flightCts.Token).Forget();

        using CompositeDisposable flightDisposables = new();

        birdEntityView.ColliderView.Collided
            .Take(1)
            .Subscribe(_ =>
            {
                _birdCollided.OnNext(birdEntityView);
                flightCts.Cancel();
            })
            .AddTo(flightDisposables);

        Transform birdTransform = birdEntityView.transform;

        while (!flightCts.IsCancellationRequested)
        {
            flyerView.LookAtVelocityDirection();

            if (birdTransform.position.y < _minYPosition)
            {
                _birdFellOut.OnNext(birdEntityView);
                _birdDestroyer.DestroyImmediate(birdEntityView);
                flightCts.Cancel();

                break;
            }

            await UniTask.Yield(flightCts.Token);
        }
    }
}
