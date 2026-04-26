using Cysharp.Threading.Tasks;
using R3;
using System;
using UnityEngine;

public class BirdFlyer : IDisposable
{
    private readonly BirdSettings _birdSettings;
    private readonly Subject<Unit> _birdCollided = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public BirdFlyer(BirdSettings birdSettings) => _birdSettings = birdSettings;

    public Observable<Unit> BirdCollided => _birdCollided;

    public void Dispose() => _compositeDisposable.Dispose();

    public void StartFlight(BirdEntityView birdEntityView)
    {
        if (birdEntityView == null)
            return;

        FlyAsync(birdEntityView).Forget();
    }

    public async UniTask FlyAsync(BirdEntityView birdEntityView)
    {
        Rigidbody birdRigidbody = birdEntityView.FlyerView.Rigidbody;

        Observable.EveryUpdate()
            .TakeUntil(birdEntityView.ColliderView.Collided)
            .Subscribe(onNext: _ =>
            {
                if (birdRigidbody.linearVelocity.sqrMagnitude > _birdSettings.RotationSpeedThreshold)
                    birdRigidbody.transform.forward = birdRigidbody.linearVelocity.normalized;
            },
            onCompleted: result =>
            {
                if (result.IsSuccess)
                    _birdCollided.OnNext(Unit.Default);
            })
            .AddTo(_compositeDisposable);
    }
}
