using Cysharp.Threading.Tasks;
using LitMotion;
using System;
using System.Threading;
using UnityEngine;

public class BoostBirdPower : IBirdPower, IDisposable
{
    private readonly BoostPowerSettings _powerSettings;
    private readonly CancellationTokenSource _cts = new();

    public BoostBirdPower(BoostPowerSettings powerSettings) => _powerSettings = powerSettings;

    public BirdPowerType Type => BirdPowerType.Boost;

    public void Activate(BirdEntityView birdEntityView)
    {
        BirdFlyerView flyerView = birdEntityView.FlyerView;
        BoostAsync(flyerView).Forget();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask BoostAsync(BirdFlyerView birdFlyerView)
    {
        birdFlyerView.StretchAsync(birdFlyerView.destroyCancellationToken).Forget();

        Vector3 linearVelocity = birdFlyerView.Rigidbody.linearVelocity;
        Vector3 targetVelocity = _powerSettings.BoostVelocity * linearVelocity.normalized;

        await LMotion.Create(linearVelocity, targetVelocity, _powerSettings.VelocityIncreasingDuration)
            .WithEase(_powerSettings.VelocityIncreasingEase)
            .Bind(velocity => birdFlyerView.Rigidbody.linearVelocity = velocity)
            .ToUniTask(_cts.Token);

        await LMotion.Create(targetVelocity, targetVelocity, _powerSettings.BoostDuration)
            .Bind(velocity => birdFlyerView.Rigidbody.linearVelocity = velocity)
            .ToUniTask(_cts.Token);
    }
}
