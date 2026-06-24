using Cysharp.Threading.Tasks;
using LitMotion;
using System;
using System.Threading;
using UnityEngine;

public class BoostBirdPower : IBirdPower, IDisposable
{
    private readonly BirdStretchSettings _stretchSettings;
    private readonly CancellationTokenSource _cts = new();

    public BoostBirdPower(BirdStretchSettings stretchSettings) => _stretchSettings = stretchSettings;

    public BirdPowerType Type => BirdPowerType.Boost;

    public void Activate(BirdEntityView birdEntityView, BirdPowerSettings powerSettings)
    {
        BirdFlyerView flyerView = birdEntityView.FlyerView;
        BoostAsync(flyerView, powerSettings).Forget();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask BoostAsync(BirdFlyerView birdFlyerView, BirdPowerSettings powerSettings)
    {
        birdFlyerView.StretchAsync(_stretchSettings, birdFlyerView.destroyCancellationToken).Forget();

        Vector3 linearVelocity = birdFlyerView.Rigidbody.linearVelocity;
        Vector3 targetVelocity = powerSettings.BoostVelocity * linearVelocity.normalized;

        await LMotion.Create(linearVelocity, targetVelocity, powerSettings.VelocityIncreasingDuration)
            .WithEase(powerSettings.VelocityIncreasingEase)
            .Bind(velocity => birdFlyerView.Rigidbody.linearVelocity = velocity)
            .ToUniTask(_cts.Token);

        await LMotion.Create(targetVelocity, targetVelocity, powerSettings.BoostDuration)
            .Bind(velocity => birdFlyerView.Rigidbody.linearVelocity = velocity)
            .ToUniTask(_cts.Token);
    }
}
