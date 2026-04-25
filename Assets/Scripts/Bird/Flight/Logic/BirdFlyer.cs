using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class BirdFlyer
{
    private readonly BirdSettings _birdSettings;
    private readonly Subject<Unit> _birdCollided = new();

    public BirdFlyer(BirdSettings birdSettings) => _birdSettings = birdSettings;

    public Observable<Unit> BirdCollided => _birdCollided;

    public void StartFlight(BirdFlyerView birdFlyerView)
    {
        if (birdFlyerView == null)
            return;

        FlyAsync(birdFlyerView).Forget();
    }

    public async UniTask FlyAsync(BirdFlyerView birdFlyerView)
    {
        Rigidbody birdRigidbody = birdFlyerView.Rigidbody;

        while (!birdFlyerView.Collided.CurrentValue)
        {
            if (birdRigidbody.linearVelocity.sqrMagnitude > _birdSettings.RotationSpeedThreshold)
                birdRigidbody.transform.forward = birdRigidbody.linearVelocity.normalized;

            await UniTask.Yield();
        }

        _birdCollided.OnNext(Unit.Default);
    }
}
