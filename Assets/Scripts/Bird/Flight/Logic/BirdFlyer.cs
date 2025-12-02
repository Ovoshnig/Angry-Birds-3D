using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class BirdFlyer
{
    private readonly BirdSettings _birdSettings;
    private readonly Subject<Unit> _birdCollided = new();

    public BirdFlyer(BirdSettings birdSettings) => _birdSettings = birdSettings;

    public Observable<Unit> BirdCollided => _birdCollided;

    public void StartFlight(Rigidbody birdRigidbody) => 
        FlyAsync(birdRigidbody).Forget();

    public async UniTask FlyAsync(Rigidbody birdRigidbody)
    {
        if (birdRigidbody == null)
            return;

        BirdFlyerView birdFlyerView = birdRigidbody.GetComponent<BirdFlyerView>();

        while (!birdFlyerView.Collided.CurrentValue)
        {
            if (birdRigidbody.linearVelocity.sqrMagnitude > _birdSettings.RotationSpeedThreshold)
                birdFlyerView.transform.forward = birdRigidbody.linearVelocity.normalized;

            await UniTask.Yield();
        }

        _birdCollided.OnNext(Unit.Default);
    }
}
